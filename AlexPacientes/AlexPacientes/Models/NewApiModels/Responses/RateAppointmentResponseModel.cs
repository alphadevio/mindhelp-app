using Newtonsoft.Json;
using System.Collections.Generic;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public partial class RateResponseModel:ResponseBaseModel<RateListModel>
    {
    }

    public partial class RateListModel
    {
        [JsonProperty("items")]
        public List<Rate> Items { get; set; }
    }

    public partial class Rate
    {
    }
}
