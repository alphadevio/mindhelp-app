using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class EditUserResponseModel : ResponseBaseModel<UserEditedListModel>
    {
    }

    public class UserEditedListModel
    {
        public List<UserEditedModel> Items { get; set; }
    }

    public class UserEditedModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
        public string TimeZone { get; set; }
        public string AccountStatus { get; set; }
        [JsonProperty("photo_media_id")]
        public int PhotoMediaID { get; set; }
    }
}
