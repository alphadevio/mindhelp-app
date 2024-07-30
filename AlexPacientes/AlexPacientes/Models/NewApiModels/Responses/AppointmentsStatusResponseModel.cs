using Newtonsoft.Json;
using System.Collections.Generic;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class Appointments : ResponseBaseModel<AppointmentsListModel>
    {

    }

    public class AppointmentsListModel
    {
        [JsonProperty("items")]
        public List<Appointment> Items { get; set; }
    }

    public class Appointment
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

        [JsonProperty("appointment_cancelled_by")]
        public string AppointmentCancelledBy { get; set; }

        [JsonProperty("patient_connected")]
        public bool PatientConnected { get; set; }

        [JsonProperty("doctor_connected")]
        public bool DoctorConnected { get; set; }

        [JsonProperty("patient_user_id")]
        public UserId PatientUserId { get; set; }

        [JsonProperty("doctor_user_id")]
        public UserId DoctorUserId { get; set; }

        [JsonProperty("promo_code")]
        public object PromoCode { get; set; }

        [JsonProperty("review")]
        public Review Review { get; set; }
    }

    public partial class UserId
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_verified_at")]
        public long EmailVerifiedAt { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("birth_at")]
        public long BirthAt { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("user_description")]
        public string UserDescription { get; set; }

        [JsonProperty("user_description_short")]
        public string UserDescriptionShort { get; set; }

        [JsonProperty("account_status")]
        public string AccountStatus { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street_address")]
        public string StreetAddress { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_code_flag")]
        public string CountryCodeFlag { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("photo_media_id")]
        public long PhotoMediaId { get; set; }

        [JsonProperty("resume_media_id")]
        public object ResumeMediaId { get; set; }

        [JsonIgnore]
        public string FullName { get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            } 
        }
    }

    public partial class Review
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("appointment_id")]
        public long AppointmentId { get; set; }
    }

}
