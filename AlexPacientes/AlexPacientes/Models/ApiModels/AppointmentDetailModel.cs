
namespace AlexPacientes.Models.ApiModels
{
    public class AppointmentDetailModel
    {
        public BookingAppointmentDetailModel Booking { get; set; }
    }

    public class BookingAppointmentDetailModel
    {
        public string TimeZone { get; set; }
        [Newtonsoft.Json.JsonProperty("doc_id")]
        public int DoctorID { get; set; }
        public string Date { get; set; }
        public string PromoCode { get; set; }
        [Newtonsoft.Json.JsonProperty("cat_id")]
        public int CategoryID { get; set; }
        public UserDoctorModel User { get; set; }
        public string BookingDate { get; set; }
        public string BookingTime { get; set; }
        public BookingConsultationDetails ConsultationDetails { get; set; }
    }

    public class BookingConsultationDetails
    {
        public string Video { get; set; }
        public string Chat { get; set; }
    }
}