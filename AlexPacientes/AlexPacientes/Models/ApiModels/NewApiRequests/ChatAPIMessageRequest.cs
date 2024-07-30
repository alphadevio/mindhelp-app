using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels.NewApiRequests
{
    public class ChatAPIMessageRequest
    {
        public int appointment_id { get; set; }
        public int patient_user_id { get; set; }
        public int doctor_user_id { get; set; }
        public int sent_by_user_id { get; set; }
        public string message { get; set; }
        public long chat_due_at { get; set; }
    }
}
