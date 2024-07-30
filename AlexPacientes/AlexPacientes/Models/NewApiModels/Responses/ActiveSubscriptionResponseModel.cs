using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class ActiveSubscriptionResponseModel : ResponseBaseModel<ActiveSubscriptionListModel>
    {
    }

    public class ActiveSubscriptionListModel
    {
        [JsonProperty("items")]
        public List<Subscription<Plan, AppModels.Identity>> Items { get; set; }
    }
}
