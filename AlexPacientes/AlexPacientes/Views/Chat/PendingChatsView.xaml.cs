using AlexPacientes.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Chat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PendingChatsView : BaseContentPage
    {
        public PendingChatsView()
        {
            InitializeComponent();
            BindingContext = new PendingChatsViewModel(this);
        }
    }
}