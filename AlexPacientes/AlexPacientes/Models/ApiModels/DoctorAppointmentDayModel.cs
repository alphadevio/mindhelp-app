
namespace AlexPacientes.Models.ApiModels
{
    public class DoctorAppointmentDayModel
    {
        [Newtonsoft.Json.JsonProperty("date_field")]
        public string Date { get; set; }
        [Newtonsoft.Json.JsonProperty("Total_TimeSlots")]
        public int TotalTimeSlots { get; set; }
        public int Appointments { get; set; }
    }
}
