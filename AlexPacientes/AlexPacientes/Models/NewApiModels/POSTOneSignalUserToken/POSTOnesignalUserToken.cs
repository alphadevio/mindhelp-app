using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.POSTOneSignalUserToken
{
    public class POSTOnesignalUserToken
    {

        [Newtonsoft.Json.JsonProperty("userId")]
        public int userId { get; set; }
        public string token { get; set; }
    }
}
