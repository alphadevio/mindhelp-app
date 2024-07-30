using System;

namespace AlexPacientes.Models.ApiModels
{
    public class TherapistRequestModel
    {
        [Newtonsoft.Json.JsonProperty("time_zone")]
        public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
        [Newtonsoft.Json.JsonProperty("doc_id")]
        public int DoctorID { get; set; }
    }
}
