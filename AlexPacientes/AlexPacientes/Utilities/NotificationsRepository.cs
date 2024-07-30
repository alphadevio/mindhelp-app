using AlexPacientes.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlexPacientes.Utilities
{
    public class NotificationsRepository
    {
        public static async Task<bool> SaveNotification(Models.SQLite.NotificationData notification)
        {
            var result = false;
            using (var access = new Services.DataAccess())
            {
                if (notification.UserID == 0)
                    notification.UserID = GlobalConfig.Identity != null ? GlobalConfig.Identity.ID : await access.GetUserIDSession();
                result = await access.SaveNotification(notification);
            }
            return result;
        }

        public static async Task<List<Models.SQLite.NotificationData>> GetNotifications()
        {
            var result = new List<Models.SQLite.NotificationData>();
            using (var access = new Services.DataAccess())
            {
                result = await access.GetAllNotificationsFromUser(GlobalConfig.Identity.ID);
            }
            return result;
        }
    }
}
