using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class GetAppointmentsRequestModel
    {
        public string Type { get; set; }
        public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
    }
}
