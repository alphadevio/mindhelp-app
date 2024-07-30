using System;

namespace AlexPacientes.Models.ApiModels
{
    public class DoctorAppointmentsRequestModel
    {
        public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
        [Newtonsoft.Json.JsonProperty("doc_id")]
        public int DoctorID { get; set; }
        public string Date { get; set; }
        [Newtonsoft.Json.JsonProperty("orderId")]
        public int OrderId { get; set; }
        public int PromoCode { get; set; }
        [Newtonsoft.Json.JsonProperty("cat_id")]
        public int CategoryID { get; set; }
    }
}
