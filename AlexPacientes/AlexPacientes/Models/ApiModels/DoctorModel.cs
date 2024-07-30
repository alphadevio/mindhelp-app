
namespace AlexPacientes.Models.ApiModels
{
    public class UserDoctorModel
    {
        public int ID { get; set; }
        public Xamarin.Forms.ImageSource ProfileImage { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public decimal ConsultationFee { get; set; }
        [Newtonsoft.Json.JsonProperty("fcm_id")]
        public string FCMID { get; set; }
        public DoctorModel Doctor { get; set; }
        public int CategoryID { get; set; }
    }

    public class DoctorModel
    {
        public string Resume { get; set; }
        public string Speciality { get; set; }
        public string Description { get; set; }
        public int WorkingSince { get; set; }
        public string SpecialityName { get; set; }
        public int TotalReviews { get; set; }
        public double TotalStar { get; set; }
        public decimal ConsultationFee { get; set; }
        public string CategoryName { get; set; }
    }
}
