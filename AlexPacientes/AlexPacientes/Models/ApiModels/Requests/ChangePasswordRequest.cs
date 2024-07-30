using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class ChangePasswordRequest
    {
        public int ID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
