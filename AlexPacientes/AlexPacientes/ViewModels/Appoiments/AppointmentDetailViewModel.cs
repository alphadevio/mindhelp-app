using AlexPacientes.Helpers;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Models.NewApiModels;
using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using AlexPacientes.ViewModels.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AlexPacientes.ViewModels.Appoiments
{
    public class AppointmentDetailViewModel : ViewModelBase
    {
        private bool _requestPaymentMethod;
        public bool RequestPaymentMethod
        {
            get { return _requestPaymentMethod; }
            set { _requestPaymentMethod = value;
                OnPropertyChanged();
            }
        }

        private DoctorModel _doctor;
        public DoctorModel Doctor
        {
            get { return _doctor; }
            set { _doctor = value;
                OnPropertyChanged();
            }
        }

        private string _coupon;
        public string Coupon
        {
            get { return _coupon; }
            set { _coupon = value;
                OnPropertyChanged();
            }
        }

        private string _couponStatus;
        public string CouponStatus
        {
            get { return _couponStatus; }
            set { _couponStatus = value;
                OnPropertyChanged();
            }
        }

        private Color _couponStatusColor;
        public Color CouponStatusColor
        {
            get { return _couponStatusColor; }
            set { _couponStatusColor = value;
                OnPropertyChanged();
            }
        }

        private bool _isCouponValid;
        public bool IsCouponValid
        {
            get { return _isCouponValid; }
            set { _isCouponValid = value;
                OnPropertyChanged();
            }
        }

        private decimal _discount;
        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value;
                OnPropertyChanged();
            }
        }

        private decimal _grandTotal;
        public decimal GrandTotal
        {
            get { return _grandTotal; }
            set { _grandTotal = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value;
                OnPropertyChanged();
            }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value;
                OnPropertyChanged();
            }
        }

        private List<MyCreditCardModel> _creditCards;
        public List<MyCreditCardModel> CreditCards
        {
            get { return _creditCards; }
            set { _creditCards = value;
                OnPropertyChanged();
            }
        }

        private MyCreditCardModel _selectedCreditCard;
        public MyCreditCardModel SelectedCreditCard
        {
            get { return _selectedCreditCard; }
            set {
                if (_selectedCreditCard != null)
                    _selectedCreditCard.IsSelected = false;
                _selectedCreditCard = value;
                if (_selectedCreditCard != null)
                    _selectedCreditCard.IsSelected = true;
                OnPropertyChanged();
            }
        }

        private bool _hasActiveSubscription;
        public bool HasActiveSubscription
        {
            get { return _hasActiveSubscription; }
            set { _hasActiveSubscription = value;
                OnPropertyChanged();
            }
        }

        public Command ApplyCouponCommand { get; set; }
        public Command RemoveCouponCommand { get; set; }
        public Command PlaceAppointmentCommand { get; set; }
        public Command OpenPaymentViewCommand { get; set; }

        private Subscription<Plan, Identity> _activeSubscription;
        
        public AppointmentDetailViewModel(Page context, DoctorModel doctor, DateTime start, DateTime end) : base(context)
        {
            RequireAuthentication = true;
            Doctor = doctor;
            StartTime = start;
            EndTime = end;
            GrandTotal = Doctor.Services[0].Cost;
            CouponStatus = "";
            Coupon = "";
            RequestPaymentMethod = true;

             ApplyCouponCommand = new Command(() => ApplyCoupon());
            RemoveCouponCommand = new Command(() => RemoveCoupon());
            PlaceAppointmentCommand = new Command(() => PlaceAppointment());
            OpenPaymentViewCommand = new Command(() => OpenPaymentView());

            Task.WhenAll(
                GeCreditCards(),
                CheckForActiveSubscription());

            MessagingCenter.Subscribe<Application, bool>(this, "ActiveSession", (sender, e) => GeCreditCards());
        }

        private async Task GeCreditCards()
        {
            IsBusy = true;
            try
            {
                var response = await new ApiRepository().CreditCards(GlobalConfig.Identity.ID);
                CreditCards = new List<MyCreditCardModel>();
                response.ForEach(e => {
                    e.CardIndex = response.IndexOf(e);
                    CreditCards.Add(new MyCreditCardModel(e));
                });
                SelectedCreditCard = CreditCards.FirstOrDefault(e => e.Default);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                //await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ApplyCoupon()
        {
            IsBusy = true;
            try
            {
                var request = new Models.NewApiModels.VerifyPromoCodeRequest()
                {
                    code = Coupon
                };
                var response = await new ApiRepository().VerifyPromoCode(request);
                if (response.HasValidStatus())
                {
                    // Check if is a valid coupon
                    var coupon = response.Data.Items[0];
                    if (coupon.IsValid)
                    {
                        // Calculate promo
                        decimal cost = Doctor.Services[0].Cost;
                        decimal discount = (coupon.DiscountPercentage > 0) ? cost * Convert.ToDecimal(coupon.DiscountPercentage) : coupon.FlatAmount;
                        discount = Math.Min(Math.Max(discount, 0), cost);
                        decimal grandTotal = cost - discount;
                        Discount = discount;
                        GrandTotal = (grandTotal >= 0) ? grandTotal : 0m;
                        IsCouponValid = true;
                        CouponStatus = Labels.Labels.ValidCoupon;
                        CouponStatusColor = Styles.Colors.PrimaryColor;
                        if (GrandTotal <= 0)
                            RequestPaymentMethod = false;
                        OnPropertyChanged(nameof(RequestPaymentMethod));
                    }
                    else
                    {
                        Discount = 0m;
                        GrandTotal = Doctor.Services[0].Cost;
                        IsCouponValid = false;
                        CouponStatus = Labels.Labels.InvalidCoupon;
                        CouponStatusColor = Color.Red;
                        RequestPaymentMethod = true;
                        OnPropertyChanged(nameof(RequestPaymentMethod));
                    }
                }
                else
                {
                    Discount = 0m;
                    GrandTotal = Doctor.Services[0].Cost;
                    IsCouponValid = false;
                    CouponStatus = Labels.Labels.InvalidCoupon;
                    CouponStatusColor = Color.Red;
                    RequestPaymentMethod = true;
                    OnPropertyChanged(nameof(RequestPaymentMethod));
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Discount = 0m;
                GrandTotal = Doctor.Services[0].Cost;
                IsCouponValid = false;
                CouponStatus = "";
                await DisplayError(Labels.Labels.GenericErrorMessage);
                RequestPaymentMethod = true;
                OnPropertyChanged(nameof(RequestPaymentMethod));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void RemoveCoupon()
        {
            Discount = 0m;
            GrandTotal = Doctor.Services[0].Cost;
            IsCouponValid = false;
            CouponStatus = "";
            Coupon = "";
            RequestPaymentMethod = true;
            OnPropertyChanged(nameof(RequestPaymentMethod));
        }

        private async void PlaceAppointment()
        {
            if (SelectedCreditCard == null && RequestPaymentMethod)
            {
                await DisplayError(Labels.Labels.NoPaymentMethodsAvailableDescription);
                return;
            }

            IsBusy = true;
            try
            {
                var saveAppointmentRequest = new Models.NewApiModels.SaveAppointmentRequest()
                {
                    start_at = new DateTimeOffset(StartTime.AddHours(Utilities.Utilities.GetExtraSummerHoursOfDate(StartTime))).ToUniversalTime().ToUnixTimeMilliseconds().ToString(),
                    end_at = new DateTimeOffset(EndTime.AddHours(Utilities.Utilities.GetExtraSummerHoursOfDate(EndTime))).ToUniversalTime().ToUnixTimeMilliseconds().ToString(),
                    base_price = Doctor.Services[0].Cost.ToString(),
                    final_price = GrandTotal.ToString(),
                    patient_user_id = GlobalConfig.Identity.ID.ToString(),
                    doctor_user_id = Doctor.ID.ToString(),
                };
                if (HasActiveSubscription)
                {
                    saveAppointmentRequest.promo_code = _activeSubscription.PromoCode.Code;
                    saveAppointmentRequest.index = "0";
                }
                else
                {
                    saveAppointmentRequest.index = SelectedCreditCard == null? "0" : SelectedCreditCard.CardIndex.ToString();
                    saveAppointmentRequest.promo_code = Coupon;
                }
                var response = await new ApiRepository().SaveAppointment(saveAppointmentRequest);
                if (response.HasValidStatus())
                {
                    var doctorToken = await new ApiRepository().GetDoctorToken(_doctor.ID);
                    var message = string.Format(Labels.Labels.NewServiceRequested, GlobalConfig.Identity.FirstName + " " + GlobalConfig.Identity.LastName);
                    var res = await new ApiRepository().SendNotificationToDoctor(doctorToken, message);
                    await Navigation.PopToRootAsync();
                    await App.ScrollToSpecificTabbedPage(1);
                    MessagingCenter.Send<AppointmentDetailViewModel>(this, ApiSettings.GenerigMessagingCenterMessagesSubscriptions.RefreshCurrentDates);
                }
                else
                {
                    await DisplayError(response.Error.Errors[0].Message);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CheckForActiveSubscription()
        {
            IsBusy = true;
            try
            {
                var response = await new ApiRepository().GetActiveSubscription();
                if (response.Count != 0)
                {
                    _activeSubscription = response[0];
                    var promoCodeRequest = new VerifyPromoCodeRequest()
                    {
                        code = _activeSubscription.PromoCode.Code
                    };
                    var promoStatus = await new ApiRepository().VerifyPromoCode(promoCodeRequest);
                    if (promoStatus.Data.Items[0].IsValid)
                    {
                        HasActiveSubscription = true;

                        // Calculate promo from subscription
                        var coupon = _activeSubscription.PromoCode;
                        decimal cost = Doctor.Services[0].Cost;
                        decimal discount = (coupon.DiscountPercentage > 0) ? cost * Convert.ToDecimal(coupon.DiscountPercentage) : coupon.FlatAmount;
                        discount = Math.Min(Math.Max(discount, 0), cost);
                        decimal grandTotal = cost - discount;
                        Discount = discount;
                        GrandTotal = (grandTotal >= 0) ? grandTotal : 0m;
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                HasActiveSubscription = false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OpenPaymentView()
        {
            MessagingCenter.Subscribe<PaymentMethodsViewModel>(this, "NewPaymentMethod", (sender) =>
            {
                MessagingCenter.Unsubscribe<PaymentMethodsViewModel>(this, "NewPaymentMethod");
                GeCreditCards();
            });
            var view = new Views.Settings.Payments.AddCardPaymentMethodView() { BindingContext = new Settings.PaymentMethodsViewModel(context, false) };
            await Navigation.PushAsync(view);
        }
    }
}
