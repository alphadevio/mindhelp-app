using AlexPacientes.Models.NewApiModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AlexPacientes.Models.AppModels
{
    public class Identity
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string ProfileImage => PhotoMedia?.Url ?? "IconMindPlaceholder.png";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string Zip { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeFlag { get; set; }
        public string Phone { get; set; }
        [JsonProperty("birth_at")]
        public long Birthday { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string TimeZone { get; set; } = "Asia/Kolkata";
        public string DeviceToken { get; set; }
        public string AccountStatus { get; set; }
        public int RegistrationStep { get; set; }
        public int TotalSocial { get; set; }
        public string Token { get; set; }
        public string Enckey { get; set; }
        public List<Role> Roles { get; set; }
        [JsonProperty("photo_media_id")]
        public PhotoMedia PhotoMedia { get; set; }
    }
}
