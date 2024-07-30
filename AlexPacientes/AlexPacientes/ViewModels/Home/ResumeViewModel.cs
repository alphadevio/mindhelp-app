using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Home
{
    public class ResumeViewModel : ViewModelBase
    {
        private string _resume;
        public string Resume
        {
            get { return _resume; }
            set { _resume = value;
                OnPropertyChanged();
            }
        }

        public ResumeViewModel(Page context, string resume) : base(context)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                var viewerUrl = "https://drive.google.com/viewerng/viewer?embedded=true&url=";
                resume = string.Format("{0}{1}", viewerUrl, resume);
            }

            Resume = resume;
        }
    }
}
