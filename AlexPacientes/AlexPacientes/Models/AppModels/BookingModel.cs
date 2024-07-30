using System;
using Xamarin.Forms;

namespace AlexPacientes.Models.AppModels
{
    public enum BookingStatus
    {
        Accepted, Completed, Rejected, Requested, Expired, PaymentRejected, Canceled, PartnerConnected, InProgress
    }

    public class BookingModel
    {
        public ImageSource Image { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public Models.NewApiModels.Responses.Appointment Appointment { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public Xamarin.Forms.Command BookAgain { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public Xamarin.Forms.Command Chat { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public Xamarin.Forms.Command VideoCall { get; set; }
        public Xamarin.Forms.Command Reschedule { get; set; }
    }

    public static class Statues
    {
        public static BookingStatus ParseStatus(string status)
        {
            switch (status)
            {
                case ("to_be_confirmed_by_therapist"):
                    return BookingStatus.Requested;
                case ("appointment_rejected"):
                    return BookingStatus.Rejected;
                case ("scheduled_appointment"):
                    return BookingStatus.Accepted;
                case ("payment_rejected"):
                    return BookingStatus.PaymentRejected;
                case ("appointment_cancelled"):
                    return BookingStatus.Canceled;
                case ("one_connected"):
                    return BookingStatus.PartnerConnected;
                case ("appointment_in_progress"):
                    return BookingStatus.InProgress;
                case ("appointment_finished"):
                    return BookingStatus.Completed;
                default: 
                    return BookingStatus.Requested;
            }
        }
    }
}
