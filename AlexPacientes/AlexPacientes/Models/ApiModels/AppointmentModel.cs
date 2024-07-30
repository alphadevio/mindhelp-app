using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class AppointmentModel
    {
        public int ID { get; set; }
        public string StartDate { get; set; }
        [Newtonsoft.Json.JsonProperty("cat_id")]
        public int CategoryID { get; set; }
        [Newtonsoft.Json.JsonProperty("user_id")]
        public int UserID { get; set; }
        [Newtonsoft.Json.JsonProperty("doc_id")]
        public int DoctorID { get; set; }
        [Newtonsoft.Json.JsonProperty("order_id")]
        public string OrderID { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string PromoCode { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string CategoryName { get; set; }
        public User User { get; set; }
        public DoctorDetails DoctorDetails { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Newtonsoft.Json.JsonProperty("fcm_id")]
        public string FCMID { get; set; }
        public string ProfileImage { get; set; }
        public int Blocked { get; set; }
        public int Deleted { get; set; }
    }


    public class DoctorDetails
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Newtonsoft.Json.JsonProperty("fcm_id")]
        public string FCMID { get; set; }
        public string ProfileImage { get; set; }
        public int Blocked { get; set; }
        public int Deleted { get; set; }
        public Doctor Doctor { get; set; }
    }

    public class Doctor
    {
        [Newtonsoft.Json.JsonProperty("user_id")]
        public int UserID { get; set; }
        public string Speciality { get; set; }
        public string SpecialityName { get; set; }
        public string CategoryName { get; set; }
        public decimal ConsultationFee { get; set; }
    }
}
