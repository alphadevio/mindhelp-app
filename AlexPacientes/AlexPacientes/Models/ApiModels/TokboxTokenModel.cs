using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class TokboxTokenModel
    {
        [Newtonsoft.Json.JsonProperty("session_id")]
        public string SessionID { get; set; }
        public string UserToken { get; set; }
        public string doctor_token { get; set; }
    }
}
