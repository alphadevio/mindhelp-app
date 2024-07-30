using AlexPacientes.Models.NewApiModels.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class Subscription<TPlan, TUser>
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("suscription_id")]
        public string SuscriptionId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("charge_id")]
        public string ChargeId { get; set; }

        [JsonProperty("subscription_start")]
        public long SubscriptionStart { get; set; }

        [JsonProperty("billing_cycle_start")]
        public long BillingCycleStart { get; set; }

        [JsonProperty("billing_cycle_end")]
        public long BillingCycleEnd { get; set; }

        [JsonProperty("trial_end")]
        public long TrialEnd { get; set; }

        [JsonProperty("plan_id")]
        public string PlanId { get; set; }

        [JsonProperty("last_billing_cycle_order_id")]
        public string LastBillingCycleOrderId { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("plan")]
        public TPlan Plan { get; set; }

        [JsonProperty("user")]
        public TUser User { get; set; }

        [JsonProperty("promo_code")]
        public PromoCodeModel PromoCode { get; set; }
    }
}
