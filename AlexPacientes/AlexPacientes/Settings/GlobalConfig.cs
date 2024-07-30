using AlexPacientes.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Settings
{
    public static class GlobalConfig
    {
        //Change it for the real Identity object
        public static Identity Identity { get; set; }

        public static string Token = "";

        public static string USER_PUSH_NOTIFICATION_ID { get;set; }
        public static string USER_PUSH_NOTIFICATION_TOKEN { get; set; }
        public static string APP_PUSH_NOTIFICATION_ID_CLIENT { get; set; } = "f6aac5c9-b5ca-49c0-bd51-1125b7cac158";
        public static string APP_PUSH_NOTIFICATION_ID_DOCTOR { get; set; } = "746ea361-59ed-42c8-83f2-821609e5d7b6";
        public static string API_PUSH_ROUTE { get; set; } = "https://onesignal.com/api/v1/notifications/";

        //Payment
        public static string CONEkTA_PROD_KEY = "key_aX1B6yrp1V1cmkkr7rY47ng";
        public static string CONEKTA_DEV_KEY = "key_JHEGAisbN4NohJvyE9xLexw";
        public static string CONEKTA_KEY = ApiSettings.IS_API_PRODUCTION ? CONEkTA_PROD_KEY : CONEKTA_DEV_KEY;
        public static string KEY = "secureHash";
        public static string IV = "ly667JuPSqBo6aqH";
    }
}
