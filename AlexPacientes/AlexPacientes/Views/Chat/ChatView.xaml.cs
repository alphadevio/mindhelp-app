using AlexPacientes.ViewModels.Chat;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : BaseContentPage
    {
        public ChatView(Models.AppModels.Chat.PendingChatModel chatContext)
        {
            InitializeComponent();
            BindingContext = new ChatViewModel(chatContext, this);
        }
    }
}