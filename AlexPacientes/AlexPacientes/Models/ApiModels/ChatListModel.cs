using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.ApiModels
{
    public class ChatListModel
    {
        public int id { get; set; }
        public int app_id { get; set; }
        public int user_id { get; set; }
        public string message { get; set; }
        public int doc_id { get; set; }
        public string msgtype { get; set; }
        public string created_atz { get; set; }
        public User user { get; set; }
        public UserDoctorModel doctor { get; set; }
    }

    public class ChatsListModel
    {
        public List<ChatListModel> chat_list { get; set; }
    }
}
