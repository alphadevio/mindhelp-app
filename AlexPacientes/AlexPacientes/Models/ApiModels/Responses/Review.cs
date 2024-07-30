using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels.Responses
{
    public class Review
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string description { get; set; }
        public float star { get; set; }
        public string created_at { get; set; }
        public string created_atz { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string profile_image { get; set; }
        public string profile_image_url { get; set; }
    }

    public class Reviews
    {
        public List<Review> reviews { get; set; }
    }
}
