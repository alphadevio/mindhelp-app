using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class PlansResponseModel : ResponseBaseModel<PlanListModel>
    {
    }

    public class PlanListModel
    {
        [JsonProperty("items")]
        public List<Plan> Items { get; set; }
    }
}
