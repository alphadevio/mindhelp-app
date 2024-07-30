using System.Collections.Generic;

namespace AlexPacientes.Models.AppModels.PushNotifications
{
    public class RequestPushModel
    {
        /// <summary>
        /// should be the default app id
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        /// Destinataries list
        /// </summary>
        public List<string> include_player_ids { get; set; }

        /// <summary>
        /// Message data
        /// </summary>
        public Data data { get; set; }

        /// <summary>
        /// Must be empty if the notification is a background notification
        /// </summary>
        public Contents contents { get; set; } = new Contents() { en = "" };
        public object subtitle { get; set; } = new { en = "" };
        public object spoken_text { get; set; } = new { en = "" };
        public bool IsSilentNotification { get; set; } = true;

        /// <summary>
        /// should be false to hide the notification
        /// </summary>
        public bool shown = false;

        /// <summary>
        /// should be false to hide the notification
        /// </summary>
        public string content_available { get; set; } = "null";

        /// <summary>
        /// should be true to hide the notification
        /// </summary>
        public string mutable_content { get; set; } = "true";

        /// <summary>
        /// indicates if is background data, should be true
        /// </summary>
        public string android_background_data { get; set; } = "true";

        public string background_data { get; set; } = "true";

        public RequestPushModel Clone()
        {
            return (RequestPushModel)this.MemberwiseClone();
        }
    }
}
