using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class ReviewRequestModel
    {
        [Newtonsoft.Json.JsonProperty("star")]
        public double Rate { get; set; }
        public string Description { get; set; } = "";
        [Newtonsoft.Json.JsonProperty("doc_id")]
        public int DoctorID { get; set; }
    }
}
