using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class SendContactFormRequest
    {
        public string email { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
    }
}
