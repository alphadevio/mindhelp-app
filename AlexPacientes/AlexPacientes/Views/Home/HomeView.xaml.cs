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
    public partial class HomeView : BaseContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = new ViewModels.Home.HomeViewModel(this);
        }
    }
}