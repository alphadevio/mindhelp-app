using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class SendRescheduleAppointmentRequest
    {
        public string start_at { get; set; }
        public string end_at { get; set; }
    }
}
