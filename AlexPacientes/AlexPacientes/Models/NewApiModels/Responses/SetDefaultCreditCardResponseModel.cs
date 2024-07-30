using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class SetDefaultCreditCardResponseModel : ResponseBaseModel<SetDefaultCreditCardListModel>
    {
    }

    public class SetDefaultCreditCardListModel
    {
        [JsonProperty("items")]
        public List<SetDefaultCreditCardItem> Items { get; set; }
    }

    public class SetDefaultCreditCardItem
    {
        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("corporate")]
        public bool Corporate { get; set; }

        [JsonProperty("custom_reference")]
        public string CustomReference { get; set; }

        [JsonProperty("default_payment_source_id")]
        public string DefaultPaymentSourceId { get; set; }

        [JsonProperty("payment_sources")]
        public PaymentSources PaymentSources { get; set; }
    }

    public class PaymentSources
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("data")]
        public List<CreditCard> Sources { get; set; }
    }
}
