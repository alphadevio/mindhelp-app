using System;

namespace AlexPacientes.Models.ApiModels.Requests
{
    public class ChatsRequestModel
    {
        //[JsonProperty("time_zone")]
        public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
    }
}
