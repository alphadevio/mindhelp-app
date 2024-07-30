using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels.Requests
{
    public class ContactUsModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Description))
                return false;
            return true;
        }
    }
}
