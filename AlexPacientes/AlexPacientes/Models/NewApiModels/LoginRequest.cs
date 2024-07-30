using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
