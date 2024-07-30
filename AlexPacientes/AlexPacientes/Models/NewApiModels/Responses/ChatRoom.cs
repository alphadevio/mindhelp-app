using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class ChatRoomResponseModel:ResponseBaseModel<ChatRoomListModel>
    {
    }

    public class ChatRoomListModel
    {
        [JsonProperty("items")]
        public List<ChatRoom> Items { get; set; }
    }

    public class ChatRoom
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("due_at")]
        public long DueAt { get; set; }

        [JsonProperty("patient_user_id")]
        public UserId PatientUserId { get; set; }

        [JsonProperty("doctor_user_id")]
        public UserId DoctorUserId { get; set; }
    }

    public partial class UserId
    {
        
    }

    public class ChatMessagesResponseModel:ResponseBaseModel<ChatMessagesListModel>
    {
    }

    public class ChatMessagesListModel
    {
        [JsonProperty("items")]
        public List<MessageModel> Items { get; set; }
    }

    public class MessageModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("chat_room_id")]
        public long ChatRoomId { get; set; }

        [JsonProperty("sent_by_user_id")]
        public SentByUserId SentByUserId { get; set; }

        [JsonProperty("attachment_media_file")]
        public object AttachmentMediaFile { get; set; }

        //LocalProperties
        [JsonIgnore]
        public Xamarin.Forms.ImageSource UserImage { get; set; }
        [JsonIgnore]
        public bool IsPasient { get; set; }
        [JsonIgnore]
        public bool NeedDisplayPicture { get; set; }

        public void SetDemoData(bool isDoctor)
        {
            if (isDoctor)
            {
                UserImage = Styles.Icons.TestDoctor;
                IsPasient = false;
            }
            else
            {
                UserImage = Styles.Icons.TestClient;
                IsPasient = true;
            }
        }
    }

    public partial class SentByUserId
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_verified_at")]
        public long EmailVerifiedAt { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("birth_at")]
        public long BirthAt { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("user_description")]
        public string UserDescription { get; set; }

        [JsonProperty("user_description_short")]
        public string UserDescriptionShort { get; set; }

        [JsonProperty("working_since")]
        public long WorkingSince { get; set; }

        [JsonProperty("account_status")]
        public string AccountStatus { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("street_address")]
        public string StreetAddress { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_code_flag")]
        public string CountryCodeFlag { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("photo_media_id")]
        public object PhotoMediaId { get; set; }

        [JsonProperty("resume_media_id")]
        public object ResumeMediaId { get; set; }
    }
}
