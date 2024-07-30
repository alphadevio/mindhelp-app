using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.GetNotificationTokens.DoctorNotificationToken
{
    public class Item
    {
        public int id { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public int doctorId { get; set; }
        public string token { get; set; }
    }
}
