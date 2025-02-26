﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlexPacientes.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : BaseContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Login.LoginViewModel(this);
        }
    }
}