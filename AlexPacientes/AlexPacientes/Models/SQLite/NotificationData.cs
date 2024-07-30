using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.SQLite
{
    [Table ("Notifications")]
    public class NotificationData
    {
        [PrimaryKey, NotNull, AutoIncrement, Column("notification_id")]
        public int ID { get; set; }
        [Column("user_id")]
        public int UserID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
