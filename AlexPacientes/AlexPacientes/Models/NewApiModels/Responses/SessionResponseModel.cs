using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class SessionResponseModel:ResponseBaseModel<SessionListModel>
    {
    }

    public class SessionListModel
    {
        [JsonProperty("items")]
        public List<Session> Items { get; set; }
    }

    public class Session
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("start_at")]
        public long StartAt { get; set; }

        [JsonProperty("end_at")]
        public long EndAt { get; set; }

        [JsonProperty("base_price")]
        public long BasePrice { get; set; }

        [JsonProperty("final_price")]
        public long FinalPrice { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("conference_session_token")]
        public string ConferenceSessionToken { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("appointment_cancelled_by")]
        public string AppointmentCancelledBy { get; set; }

        [JsonProperty("patient_connected")]
        public bool PatientConnected { get; set; }

        [JsonProperty("doctor_connected")]
        public bool DoctorConnected { get; set; }

        [JsonProperty("patient_user_id")]
        public long PatientUserId { get; set; }

        [JsonProperty("doctor_user_id")]
        public long DoctorUserId { get; set; }

        [JsonProperty("promo_code")]
        public object PromoCode { get; set; }

        [JsonProperty("review")]
        public long Review { get; set; }
    }
}
