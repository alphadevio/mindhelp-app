using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class PaymentKeyResponseModel : ResponseBaseModel<PaymentKeyListModel>
    {
    }

    public class PaymentKeyListModel
    {
        [JsonProperty("items")]
        public List<PaymentKey> Items { get; set; }
    }

    public class PaymentKey
    {
        [JsonProperty("publicApiKey")]
        public string Key { get; set; }
    }
}
