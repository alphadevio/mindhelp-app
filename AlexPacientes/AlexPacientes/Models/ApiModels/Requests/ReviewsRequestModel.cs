using System;

namespace AlexPacientes.Models.ApiModels
{
    public class ReviewsRequestModel
    {
        [Newtonsoft.Json.JsonProperty("pageno")]
        public int Page { get; set; } = 1;
        [Newtonsoft.Json.JsonProperty("pageoffset")]
        public int Offset { get; set; } = 30;
        public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
        [Newtonsoft.Json.JsonProperty("doc_id")]
        public int DoctorID { get; set; }
    }
}
