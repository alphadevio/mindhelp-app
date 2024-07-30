using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class DoctorsResponseModel : ResponseBaseModel<DoctorListModel>
    {
    }

    public class DoctorListModel
    {
        public List<DoctorModel> Items { get; set; }
    }

    public class DoctorModel
    {
        public int ID { get; set; }
        public string ProfileImage => PhotoMedia?.Url ?? "IconMindPlaceholder.png";
        public string Name => string.Format("{0} {1}", FirstName, LastName);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Service<int, int>> Services { get; set; }
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
        public string UserDescription { get; set; }
        public string UserDescriptionShort { get; set; }
        [JsonProperty("photo_media_id")]
        public PhotoMedia PhotoMedia { get; set; }
        [JsonProperty("resume_media_id")]
        public ResumeMedia ResumeMedia { get; set; }

        [JsonIgnore]
        public ICommand DoctorSelectedCommand { get; set; }

        [JsonProperty("time_slots")]
        public List<AvailabilityTimeSlotModel> TimeSlots { get; set; }

        [JsonProperty("availability_month")]
        public int AvailabilityMonth { get; set; }
    }
}
