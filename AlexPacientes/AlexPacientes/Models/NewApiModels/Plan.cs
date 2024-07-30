using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class Plan
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("conekta_id")]
        public string ConektaId { get; set; }

        [JsonProperty("plan_id")]
        public string PlanId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("interval")]
        public string Interval { get; set; }

        [JsonProperty("frequency")]
        public long Frequency { get; set; }

        [JsonProperty("trial_period_days")]
        public long TrialPeriodDays { get; set; }

        [JsonProperty("expiry_count")]
        public long ExpiryCount { get; set; }

        [JsonProperty("maximum_number_of_applications")]
        public long MaximumNumberOfApplications { get; set; }

        [JsonProperty("discount_percentage")]
        public double DiscountPercentage { get; set; }

        [JsonProperty("flat_amount")]
        public long FlatAmount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isPreferred")]
        public bool IsPreferred { get; set; }

        //Local variables
        public bool IsActive { get; set; }
        public string ExtraDescription { get; set; }
    }
}
