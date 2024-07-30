using Newtonsoft.Json;
using System.Collections.Generic;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public partial class CreditCardResponse : ResponseBaseModel<CreditCardListModel>
    {
    }

    public partial class CreditCardListModel
    {
        [JsonProperty("items")]
        public List<CreditCard> Items { get; set; }
    }
    public partial class CreditCard
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }

        [JsonProperty("bin")]
        public long Bin { get; set; }

        [JsonProperty("exp_month")]
        public string ExpMonth { get; set; }

        [JsonProperty("exp_year")]
        public string ExpYear { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        //Local properties, must not be deleted
        public Xamarin.Forms.Command DeleteCardCommand { get; set; }
        public int CardIndex { get; set; }
    }

}
