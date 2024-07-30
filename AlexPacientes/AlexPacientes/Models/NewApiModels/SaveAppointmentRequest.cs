using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class SaveAppointmentRequest
    {
        public string start_at { get; set; }
        public string end_at { get; set; }
        public string base_price { get; set; }
        public string final_price { get; set; }
        public string patient_user_id { get; set; }
        public string doctor_user_id { get; set; }
        public string index { get; set; }
        public string promo_code { get; set; }
    }
}
