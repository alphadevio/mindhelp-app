using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class PostTokenRequest
    {
        [Newtonsoft.Json.JsonProperty("token")]
        public string Token { get; set; }
    }
}
