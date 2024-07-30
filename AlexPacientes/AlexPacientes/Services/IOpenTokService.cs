
namespace AlexPacientes.Services
{
    public interface IOpenTokService
    {
        void InitNewSession(string apiKey, string sessionID, string token);
        void EndSession();
        void SetHostContainer(Controls.StreamView streamView);
        void SetClientContainer(Controls.StreamView streamView);
        void Connect();
        void PublishAudio(bool audio);
        void PublishVideo(bool video);
        void SwapCamera();
        Xamarin.Forms.Command OnPartnerConnectedCommand { get; set; }
    }
}
