using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using AlexPacientes.Views.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes
{
    public partial class App : Application
    {
        public static OneSignalPushNotificationHandler PushNotificationHandler;
        public static NavigationTabbedPage NavigationTabbedPage { get; set; }
        public App()
        {
            InitializeComponent();
            NavigationTabbedPage = new NavigationTabbedPage();
            MainPage = new NavigationPage(NavigationTabbedPage);
            NavigationPage.SetHasNavigationBar(NavigationTabbedPage, false); 
            PushNotificationHandler = new OneSignalPushNotificationHandler();
            using (var access = new Services.DataAccess()) { }//Force Tables Creation
            Device.BeginInvokeOnMainThread(async () =>
            {
                using (var access = new Services.DataAccess())
                {
                    var creds = await access.GetUserSessionCredentials();
                    if (creds != null)
                    {
                        await access.DeleteAllSessions();
                        await Authenticate(creds);
                    }
                }
            });
        }

        public static async Task<Tuple<bool,string>> Authenticate(Models.AppModels.Identity identity)
        {
            try
            {
                var data = new Models.NewApiModels.LoginRequest
                {
                    email=identity.Email,
                    password=identity.Password
                };
                var response = await new ApiRepository().SignIn(data);
                if (response.HasValidStatus())
                {
                    using(var access=new Services.DataAccess())
                    {
                        identity.ID = response.Data.Items[0].ID;
                        await access.SaveUserSession(identity);
                    }
                    var rest = new RestClient();
                    // Get user
                    var user = response.Data.Items[0];
                    var tokenData = new Models.NewApiModels.POSTOneSignalUserToken.POSTOnesignalUserToken()
                    {
                        token = GlobalConfig.USER_PUSH_NOTIFICATION_ID,
                        userId = user.ID
                    };
                    GlobalConfig.Identity = user;
                    GlobalConfig.Token = user.Token;
                    GlobalConfig.USER_PUSH_NOTIFICATION_TOKEN = await DependencyService.Get<Services.IFirebaseToken>()?.GetToken();
                    //Verify if we already have the notification token
                    var tokenResponse = await new ApiRepository().PostNotificationToken(user.ID, GlobalConfig.USER_PUSH_NOTIFICATION_TOKEN);
                    if (tokenResponse || true) //push notification token not working
                    {
                        MessagingCenter.Send<Application, bool>(App.Current, "ActiveSession", true);
                        return new Tuple<bool, string>(true, string.Empty);
                    }
                    GlobalConfig.Identity = null;
                    GlobalConfig.Token = null;
                    return new Tuple<bool, string>(false, Labels.Labels.GenericErrorMessage);
                }
                else
                {
                    return new Tuple<bool, string>(false, response.Error.Errors[0].Message);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return new Tuple<bool, string>(false, Labels.Labels.GenericErrorMessage);
            }
        }

        public static async Task Logout()
        {
            //Delete session and logout
            //App.Current.MainPage = new NavigationPage(new Views.Login.LoginView());
            MessagingCenter.Send<Application, bool>(App.Current, "ActiveSession", false);
            GlobalConfig.Identity = null;
            GlobalConfig.Token = string.Empty;
            using (var access = new Services.DataAccess())
            {
                await access.DeleteAllSessions();
            }
        }

        /// <summary>
        /// Navigate to the given page from the TabbedPage context.
        /// </summary>
        /// <param name="page">Page to push</param>
        /// <returns></returns>
        public static async Task NavigateFromTabbedPage(Page page)
        {
            await Current.MainPage.Navigation.PushAsync(page);
        }

        public static async Task ScrollToSpecificTabbedPage(int tabbedPageIndex, Page PageToNavigateOnSelectedTab=null)
        {
            if (App.NavigationTabbedPage.Children.Count <= tabbedPageIndex) return;
            App.NavigationTabbedPage.CurrentPage = NavigationTabbedPage.Children[tabbedPageIndex];
            if (PageToNavigateOnSelectedTab != null)
                await NavigationTabbedPage.Children[tabbedPageIndex].Navigation.PushAsync(PageToNavigateOnSelectedTab);
        }

        /// <summary>
        /// Return to TabbedPage context
        /// </summary>
        public static async Task ReturnToTabbedPage()
        {
            await Current.MainPage.Navigation.PopToRootAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
