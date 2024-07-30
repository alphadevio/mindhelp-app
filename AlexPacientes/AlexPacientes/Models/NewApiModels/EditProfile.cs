using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels
{
    public class EditProfile
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string birth_at { get; set; }
        public string gender { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string street_address { get; set; }
        public string zip { get; set; }
        public string country_code { get; set; }
        public string country_code_flag { get; set; }
        public string time_zone { get; set; }
    }
    public class EditProfileExtention:EditProfile
    {
        public string base64 { get; set; }
        public string base64_extension { get; set; }
        public void setValues(EditProfile e)
        {
            first_name = e.first_name;
            last_name = e.last_name;
            phone = e.phone;
            birth_at = e.birth_at;
            gender = e.gender;
            country = e.country;
            state = e.state;
            city = e.city;
            street_address = e.street_address;
            zip = e.zip;
            country_code = e.country_code;
            country_code_flag = e.country_code_flag;
            time_zone = e.time_zone;
        }
    }
}
