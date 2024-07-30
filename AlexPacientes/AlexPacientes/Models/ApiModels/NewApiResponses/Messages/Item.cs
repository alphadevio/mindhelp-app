using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels.NewApiResponses.Messages
{
    public class Item
    {
        public int id { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public bool deleted { get; set; }
        public long chat_due_at { get; set; }
        public string message { get; set; }
        public int appointment_id { get; set; }
        public int sent_by_user_id { get; set; }
        public int patient_user_id { get; set; }
        public int doctor_user_id { get; set; }
    }
}
