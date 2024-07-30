using AlexPacientes.Models.AppModels.Payments;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using AlexPacientes.Views.Settings.Payments;
using Conekta.Xamarin;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Settings
{
    public class PaymentMethodsViewModel:ViewModelBase
    {
        private PaymentCardModel _card;
        public PaymentCardModel Card
        {
            get { return _card; }
            set { _card = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.NewApiModels.Responses.CreditCard> _creditCards;
        public ObservableCollection<Models.NewApiModels.Responses.CreditCard> CreditCards
        {
            get { return _creditCards; }
            set { _creditCards = value;
                OnPropertyChanged();
            }
        }

        public Command DisplayAddCreditCardViewCommand { get; set; }
        public Command AddCreditCardCommand { get; set; }
        public Command DeleteCardCommand { get; set; }
        public Command SetDefaultCardCommand { get; set; }

        private bool _reloadCreditCards;

        public PaymentMethodsViewModel(Page context, bool reloadCreditCards = true):base(context)
        {
            _reloadCreditCards = reloadCreditCards;
            DisplayAddCreditCardViewCommand = new Command(async() => await OnDisplayAddCreditCardView());
            AddCreditCardCommand = new Command(async () => await OnAddCreditCard());
            SetDefaultCardCommand = new Command(async (param) => await OnSetDefaultCard((Models.NewApiModels.Responses.CreditCard)param));
            Card = new PaymentCardModel();
            CreditCards = new ObservableCollection<Models.NewApiModels.Responses.CreditCard>();

            if (reloadCreditCards)
                _ = GetPaymentMethods();
        }

        
        async Task OnDisplayAddCreditCardView()
        {
            var page = new AddCardPaymentMethodView();
            page.BindingContext = this;
            await Navigation.PushAsync(page);
        }
        async Task GetPaymentMethods()
        {
            try
            {
                IsBusy = true;
                CreditCards.Clear();
                var cards = await new ApiRepository().CreditCards(GlobalConfig.Identity.ID);
                int index = 0;
                cards.ForEach(e => {
                    e.CardIndex = index;
                    e.DeleteCardCommand=new Command(async(p)=> await OnDeleteCard(p as Models.NewApiModels.Responses.CreditCard));
                    CreditCards.Add(e);
                    index++;
                });
                OnPropertyChanged("CreditCards");
            }
            catch { }
            IsBusy = false;
        }

        async Task OnAddCreditCard()
        {
            if (!Card.Validate())
            {
                await DisplayError(Labels.Labels.AllFieldsRequired);
                return;
            }
            IsBusy = true;
            try
            {
                var addStatus = await new ApiRepository().AddCreditCard(GlobalConfig.Identity.ID, Card);
                if (addStatus)
                {
                    if (_reloadCreditCards)
                        await GetPaymentMethods();
                    
                    MessagingCenter.Send<PaymentMethodsViewModel>(this, "NewPaymentMethod");

                    await Navigation.PopAsync();
                }
                else
                    await DisplayError(Labels.Labels.PaymentMethodAddedError);
            }
            catch
            {
                await DisplayError(Labels.Labels.PaymentMethodAddedError);
            }
            IsBusy = false;
        }
        async Task OnDeleteCard(Models.NewApiModels.Responses.CreditCard card)
        {
            IsBusy = true;
            try
            {
                var deleteStatus = await new ApiRepository().DeleteCreditCard(GlobalConfig.Identity.ID, card.CardIndex);
                if (deleteStatus)
                {
                    await GetPaymentMethods();
                }
                else
                    await DisplayError(Labels.Labels.ErrorDeletingPaymentMethod);
            }
            catch { await DisplayError(Labels.Labels.ErrorDeletingPaymentMethod); }
            IsBusy = false;
        }

        async Task OnSetDefaultCard(Models.NewApiModels.Responses.CreditCard card)
        {
            if (card.Default)
                return;
            var confirmation = await Alert.DisplayAlert(string.Empty, Labels.Labels.CreditCardDefaultQuestion, Labels.Labels.OK, Labels.Labels.Cancel);
            if (!confirmation) 
                return;

            IsBusy = true;
            try
            {
                var respose = await new ApiRepository().SetDefaultCreditCard(GlobalConfig.Identity.ID, card.CardIndex);
                if (respose)
                {
                    await GetPaymentMethods();
                }
                else
                    await DisplayError(Labels.Labels.ErrorDeletingPaymentMethod);
            }
            catch { await DisplayError(Labels.Labels.ErrorDeletingPaymentMethod); }
            IsBusy = false;
        }
    }
}
