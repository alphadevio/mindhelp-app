using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Settings
{
    public static class ApiSettings
    {
        //private const string BASE_URL_PRODUCTION = "https://mindhelp.mx/"; 
        //private const string BASE_URL_SANDBOX = "http://13.52.37.75/";
        public const string BASE_NEW_API_SANDBOX = "https://mindhelp.alphadev.io/";//"https://mindhelpapi.beesoftware.dev/";
        public const string BASE_NEW_API_PRODUCTION = "https://api.mindhelp.mx/";
        public const string BASE_URL = IS_API_PRODUCTION ? BASE_NEW_API_PRODUCTION : BASE_NEW_API_SANDBOX;
        public const string API_PATH = "api";
        public const string VERSION = "v1";
        public const string API_URL = BASE_URL + API_PATH + "/" + VERSION + "/";
        public const bool IS_API_PRODUCTION = true;

        public const string PUBLIC_KEY = "OnnptHpzXXyREqAbmRYx0RCgp1vMRoHA";

        // Tokbox
        public const string TOKBOX_API_KEY = "46356992";

        public static class Status
        {
            public static int SUCCESS = 200;
            public static int CREATED = 201;
            public static int VALIDATION_ERROR = 400;
            public static int ERROR = 500;
        }

        public static class ContentKeys
        {
            public const string DATA = "data";
        }

        public static class Methods
        {
            public const string LOGIN = "auth/local/signin";
            public const string GET_CATEGORIES = "system/categories";
            public const string GET_DOCTORS = "system/categories/{0}/doctors";
            public const string GET_DOCTORS_BY_CATEGORY_ADVANCED_FILTERS = "ecomm/users/{0}/time-slot-by-day?time_as_reference={1}";
            public const string GET_DOCTORS_ADVANCED_FILTERS = "ecomm/users/time-slot-by-day";
            public const string GET_DOCTOR_DETAIL = "doctor_details";
            public const string GET_REVIEWS = "users/{0}/reviews";
            public const string GET_DOCTOR_APPOINTS = "doc_appointments_list";
            public const string GET_DOCTOR_TIMES = "users/{0}/time-slots";
            public const string GET_DOCTOR_FEE = "doc_fee";
            public const string APPLY_COUPON = "system/promo-codes/verify";
            public const string SAVE_APPOINTMENT = "appointments";
            public const string GET_ALL_APPOINTMENTS = "all_appointment";
            public const string CREATE_TOKBOX_TOKEN = "create_token/3";
            public const string POST_REVIEW = "review_post";
            public const string POST_GENERAL_CHATS = "chats";
            public const string UPDATE_PROFILE = "users/{0}";
            public const string UPDATE_PASSWORD = "password_update";
            public const string REGISTER = "auth/local/signup/patient";
            public const string FORGOT_PASSWORD = "auth/reset-password/request";

            //New api methods

            public const string POST_OST = "ostusers";
            public const string PATCH_OST = "ostusers/{0}";
            public const string GET_APPOINTMENT_BY_APPOINTMENT_ID = "legacyappointments/{0}";
            public const string POST_APPOINTMENT_SESSION_DATA = "legacyappointments";
            public const string GET_DOCTOR_NOTIFICATION_TOKEN = "ostdoctors/{0}";
            public const string GET_SESSION_MESSAGES = "legacychats?where={\"appointment_id\":{0}}&sort=created_at DESC&limit={1}&skip=0";
            public const string GET_CHATS = "legacychats?where={\"patient_user_id\":{0}}";//THIS ENPOINT IS TEMPORAL, NOW IT GIVE ALL THE CHATS AND WE MAKE A DISTINCT BY DOCTORID
            
            public const string POST_CHAT_MESSAGE = "legacychats";
            public const string GET_PAYMENT_KEYS = "system/payments/keys";
            public const string GET_CREDIT_CARD = "users/{0}/payment/cards";
            public const string POST_CREDIT_CARD = "users/{0}/payment/cards";
            public const string DELETE_CREDIT_CARD = "users/{0}/payment/cards/{1}";
            public const string POST_SET_DEFAULT_CREDIT_CARD = "users/{0}/payment/cards/{1}/default";
            public const string GET_CURRENT_APPOINTMENTS = "users/{0}/appointments/status/to-confirm";
            public const string GET_ALL_NEXT_APPOINTMENTS = "users/{0}/appointments/status/scheduled";
            public const string GET_ALL_CURRENT_APPOINTMENTS = "users/{0}/appointments/status/in-progress";
            public const string GET_PAST_APPOINTMENTS = "users/{0}/appointments/status/finished";
            public const string GET_PENDING_TO_CONFIRM_APPOINTMENTS = "users/{0}/appointments/status/to-confirm";
            public const string POST_GET_CONNECTION_DATA = "users/{0}/appointments/{1}/status/connected";
            public const string POST_FINISH_MEETING = "users/{0}/appointments/{1}/status/finished";
            public const string POST_RATE_APPOINTMENT = "appointments/{0}/reviews";
            public const string GET_CHATROOMS = "users/{0}/chats";
            public const string GET_IMAGE_SOURCE_FROM_ID = "system/media/{0}";//Media ID
            public const string POST_SEND_NOTIFICATION_TOKEN = "users/{0}/tokens/notification";
            public const string POST_SEND_CHAT_MESSAGE = "users/{0}/chats/{1}/messages";
            public const string GET_CHAT_MESSAGES_HISTORY = "users/{0}/chats/{1}/messages";
            public const string POST_CONTACT_FORM = "system/emails/send-contact-form";
            public const string GET_PLANS = "system/payments/plans";
            public const string POST_SUBSCRIPTION = "users/{0}/payments/suscriptions/{1}";
            public const string GET_ACTIVE_SUBSCRIPTION = "users/{0}/payments/suscriptions/active";
            public const string POST_RESCHEDULE_APPOINTMENT = "appointments/{0}/move";
            public const string DELETE_CANCEL_SUBSCRIPTION = "users/{0}/payments/suscriptions";
            public const string DELETE_DELETE_ACCOUNT = "users/{0}";
        }

        public static class PUSH_NOTIFICATION_ACTIONS
        {
            public const string CHAT_MESSAGE = "ChatMessage";
        }

        public static class MessagingCenterMessageSubcriptions
        {
            public static string RefreshChat = "Chat need to be refreshed";
        }
        public static class GenerigMessagingCenterMessagesSubscriptions
        {
            public static string RefreshCurrentDates = "NewAppointment";
        }
    }
}