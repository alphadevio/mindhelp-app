﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResumeView : BaseContentPage
    {
        public ResumeView(string resume)
        {
            InitializeComponent();
            BindingContext = new ViewModels.Home.ResumeViewModel(this, resume);
        }
    }
}