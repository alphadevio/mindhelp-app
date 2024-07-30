using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AlexPacientes.Models.SQLite
{
    [Table("ChatSession")]
    public class ChatSession
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int PK { get; set; }
        /// <summary>
        /// should be same than the appointmentId
        /// </summary>
        public int SessionChatId { get; set; }
        /// <summary>
        /// The name of the chat, usually, Doctor name - Consultant name
        /// </summary>
        public string chatName { get; set; }
        /// <summary>
        /// Who is the owner of the chat (usually is the user that it's using the App)
        /// </summary>
        public int chatOwnerID { get; set; }
        public string chatImage { get; set; }
        public string lastMessage { get; set; }
        public string hour { get; set; }

        [OneToMany]
        private List<MessageData> messages { get; set; }
    }

    [Table("MessageData")]
    public class MessageData
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int PK { get; set; }
        public DateTime sendTime { get; set; }
        public int messageOwnerID { get; set; }
        public string message { get; set; }
        public string messageOwnerName { get; set; }
        public string ownerImage { get; set; }
        
        [ForeignKey(typeof(ChatSession)), NotNull] 
        public int chatSessionFK { get; set; }
        public int chatRoomID { get; set; }
    }
}
