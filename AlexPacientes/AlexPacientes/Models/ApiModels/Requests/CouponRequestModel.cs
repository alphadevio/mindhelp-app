using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class CouponRequestModel
    {
        [Newtonsoft.Json.JsonProperty("promo_code")]
        public string PromoCode { get; set; }
        [Newtonsoft.Json.JsonProperty("speciality_id")]
        public int SpecialityID { get; set; }
    }
}
