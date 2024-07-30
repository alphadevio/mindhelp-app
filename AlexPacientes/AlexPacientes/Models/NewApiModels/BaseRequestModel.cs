using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class BaseRequestModel
    {
        [Newtonsoft.Json.JsonProperty("message")]
        public string Message { get; set; }
    }
}
