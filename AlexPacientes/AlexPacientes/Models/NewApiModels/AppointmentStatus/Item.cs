using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.AppointmentStatus
{
    public class Item
    {
        public int id { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }

        [JsonProperty("appointmentId")]
        public int appointmentId { get; set; }

        [JsonProperty("userId")]
        public int userId { get; set; }

        [JsonProperty("doctorId")]
        public int doctorId { get; set; }
        public string session { get; set; }
        public string user_token { get; set; }
        public string doctor_token { get; set; }
    }
}
