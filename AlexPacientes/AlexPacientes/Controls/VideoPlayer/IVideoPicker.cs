using System.Threading.Tasks;

namespace AlexPacientes.Controls.VideoPlayer
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}
