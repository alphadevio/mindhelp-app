using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class ForgotPasswordRequestModel
    {
        public int RoleID { get; set; } = 3;
        public string Email { get; set; }
    }
}
