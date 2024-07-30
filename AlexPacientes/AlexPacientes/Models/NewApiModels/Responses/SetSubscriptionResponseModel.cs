using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class SetSubscriptionResponseModel : ResponseBaseModel<SetSubscriptionListModel>
    {
    }

    public class SetSubscriptionListModel
    {
        [JsonProperty("items")]
        public List<Subscription<Plan, Models.AppModels.Identity>> Items { get; set; }
    }
}
