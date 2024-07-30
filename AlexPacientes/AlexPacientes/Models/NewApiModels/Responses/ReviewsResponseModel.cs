using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class ReviewsResponseModel : ResponseBaseModel<ReviewListModel>
    {
    }

    public class ReviewListModel
    {
        public List<ReviewModel> Items { get; set; }
    }

    public class ReviewModel
    {
        public string ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public long CreatedAt { get; set; }
        [Newtonsoft.Json.JsonProperty("score")]
        public double Rate { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Name => string.Format("{0} {1}", FirstName, LastName);
        [Newtonsoft.Json.JsonIgnore]
        public DateTime Date => DateTimeOffset.FromUnixTimeMilliseconds(CreatedAt).DateTime;
    }
}
