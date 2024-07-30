
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class NotificationTokenResponseModel : ResponseBaseModel<NotificationTokenListModel>
    {
    }

    public class NotificationTokenListModel
    {
        [JsonProperty("items")]
        public List<NotificationToken> Items { get; set; }
    }

    public class NotificationToken
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("user")]
        public long User { get; set; }
    }
}
