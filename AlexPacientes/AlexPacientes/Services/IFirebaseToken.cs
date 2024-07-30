
namespace AlexPacientes.Services
{
    public interface IFirebaseToken
    {
        System.Threading.Tasks.Task<string> GetToken();
    }
}
