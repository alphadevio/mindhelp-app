using AlexPacientes.Models.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class PromoCodeResponseModel : ResponseBaseModel<PromoCodeListModel>
    {
    }

    public class PromoCodeListModel
    {
        public List<PromoCodeModel> Items { get; set; }
    }

    public class PromoCodeModel
    {
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("start_at")]
        public long StartAt { get; set; }

        [JsonProperty("end_at")]
        public long EndAt { get; set; }

        [JsonProperty("maximum_number_of_applications")]
        public long MaximumNumberOfApplications { get; set; }

        [JsonProperty("discount_percentage")]
        public double DiscountPercentage { get; set; }

        [JsonProperty("flat_amount")]
        public long FlatAmount { get; set; }

        [JsonProperty("isSuscription")]
        public bool IsSuscription { get; set; }
    }
}
