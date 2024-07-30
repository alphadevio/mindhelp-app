using AlexPacientes.Services;
using System;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AlexPacientes.iOS.Services.FirebaseToken))]
namespace AlexPacientes.iOS.Services
{
    public class FirebaseToken : IFirebaseToken
    {
        public Task<string> GetToken()
        {
            return Task.FromResult(Firebase.CloudMessaging.Messaging.SharedInstance.FcmToken);
        }
    }
}