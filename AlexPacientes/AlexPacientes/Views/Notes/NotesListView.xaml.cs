using AlexPacientes.ViewModels.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListView : BaseContentPage
    {
        public NotesListView()
        {
            InitializeComponent();
            BindingContext = new NotesListViewModel(this);
        }
    }
}