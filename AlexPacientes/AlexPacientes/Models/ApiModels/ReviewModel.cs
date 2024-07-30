
using System;

namespace AlexPacientes.Models.ApiModels
{
    public class ReviewModel
    {
        public string ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        [Newtonsoft.Json.JsonProperty("created_atz")]
        public string CreatedAt { get; set; }
        [Newtonsoft.Json.JsonProperty("star")]
        public double Rate { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Name => string.Format("{0} {1}", FirstName, LastName);
        [Newtonsoft.Json.JsonIgnore]
        public DateTime Date => DateTime.Parse(CreatedAt);
    }
}
