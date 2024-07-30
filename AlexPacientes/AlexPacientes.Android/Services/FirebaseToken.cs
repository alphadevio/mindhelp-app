using AlexPacientes.Services;
using Android.Gms.Extensions;

[assembly: Xamarin.Forms.Dependency(typeof(AlexPacientes.Droid.Services.FirebaseToken))]
namespace AlexPacientes.Droid.Services
{
    public class FirebaseToken : IFirebaseToken
    {
        public async System.Threading.Tasks.Task<string> GetToken()
        {
            return (await Firebase.Messaging.FirebaseMessaging.Instance.GetToken()).ToString();
        }
    }
}