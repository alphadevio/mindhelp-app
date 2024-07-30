using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.AppModels.PushNotifications
{
    public class Data
    {
        public int Action { get; set; }
        /// <summary>
        /// set if is a silent notification
        /// </summary>
        public bool IsSilentNotification { get; set; }
        /// <summary>
        /// title for push notification display
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// subtitle/body for push notification display
        /// </summary>
        public string Subtitle { get; set; }


        //------------------------    LocalMessageData   ------------------------
        public DateTime sendTime { get; set; }
        public int messageOwnerID { get; set; }
        public string message { get; set; }
        public string messageOwnerName { get; set; }
        public int chatSessionFK { get; set; }
        public string ownerImage { get; set; }
    }
}
