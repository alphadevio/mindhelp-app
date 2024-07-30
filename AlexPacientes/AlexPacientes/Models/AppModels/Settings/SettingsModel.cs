using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.AppModels.Settings
{
    public class SettingsModel
    {
        public string Title { get; set; }
        public Xamarin.Forms.Command Action { get; set; }
        public bool IsSwitch { get; set; } = false;
    }
}
