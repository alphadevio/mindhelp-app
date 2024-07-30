using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels.Requests
{
    public class UpdatePasswordRequest
    {
        public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
    }
}
