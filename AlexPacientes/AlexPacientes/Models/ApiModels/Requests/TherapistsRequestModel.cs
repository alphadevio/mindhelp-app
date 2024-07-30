using System;

namespace AlexPacientes.Models.ApiModels
{
    public class TherapistsRequestModel
    {
        [Newtonsoft.Json.JsonProperty("pageno")]
        public int Page { get; set; } = 1;
        [Newtonsoft.Json.JsonProperty("pageoffset")]
        public int Offset { get; set; } = 30;
        public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
        [Newtonsoft.Json.JsonProperty("cat_id")]
        public int CategoryID { get; set; }
    }
}
