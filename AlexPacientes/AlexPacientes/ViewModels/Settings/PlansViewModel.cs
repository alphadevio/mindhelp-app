using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plan = AlexPacientes.Models.NewApiModels.Plan;

namespace AlexPacientes.ViewModels.Settings
{
    public class PlansViewModel : ViewModelBase
    {
        public ObservableCollection<Plan> _plans;
        public ObservableCollection<Plan> Plans
        {
            get { return _plans; }
            set { _plans = value;
                OnPropertyChanged();
            }
        }

        public Command SelectPlanCommand { get; set; }

        public PlansViewModel(Page context) : base(context)
        {
            SelectPlanCommand = new Command((param) => OnPlanSelected((Plan)param)); 
            _ = GetPlans();
        }

        private async Task GetPlans()
        {
            IsBusy = true;
            try
            {
                var noSubscriptionPlan = new Plan()
                {
                    Amount = 0,
                    Currency = "usd",
                    Description = Labels.Labels.NoSubscriptionDescription,
                    Name = Labels.Labels.NoSubscription,
                    ExtraDescription=Labels.Labels.CancelExtraDescription,
                    DiscountPercentage = 0,
                    FlatAmount = 0,
                    Id= int.MinValue
                };
                var activeSubscription = await new ApiRepository().GetActiveSubscription();
                var result = await new ApiRepository().GetPlans();
                if (activeSubscription != null && activeSubscription.Count > 0)
                {
                    result.ForEach(e =>
                    {
                        if (e.PlanId == activeSubscription.FirstOrDefault().Plan.PlanId)
                        {
                            e.IsActive = true;
                        }
                        e.Amount /= 100;
                    });
                }
                else
                {
                    noSubscriptionPlan.IsActive = true;
                    result.ForEach(e => {e.Amount /= 100;});
                }
                result.Add(noSubscriptionPlan);
                result = result.OrderBy(x => x.Amount).ToList();
                Plans = new ObservableCollection<Plan>(result);
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

        private async Task OnPlanSelected(Plan plan)
        {
            string extras = "";
            if(!string.IsNullOrWhiteSpace(plan.ExtraDescription))
                extras= "\n\n\n" + plan.ExtraDescription;
            var option = await Alert.DisplayAlert(plan.Name, string.Format(Labels.Labels.SubscriptionBuyQuestion, plan.Name) + extras, Labels.Labels.Confirm, Labels.Labels.Cancel);
            if (!option) return;
            IsBusy = true;
            bool response = false;
            if (plan.Id == int.MinValue)
                response = await CancelSubscription();
            else
                response = await UpdateSubscription(plan.Id);

            if (response)
                await Alert.DisplayAlert(Labels.Labels.Congratulations, Labels.Labels.SubscriptionSuccesfulPurchase, Labels.Labels.OK);
            else
                await DisplayError(Labels.Labels.GenericErrorMessage);
            await GetPlans();
            IsBusy = false;
        }

        private async Task<bool> CancelSubscription()
        {
            try
            {
                return await new ApiRepository().CancelSubscriptio(GlobalConfig.Identity.ID);
            }
            catch { }
            return false;
        }

        private async Task<bool> UpdateSubscription(long planID)
        {
            try
            {
                return (await new ApiRepository().SetSubscription(planID)).Count > 0;
            }
            catch (Exception exc) { }
            return false;
        }
    }
}
