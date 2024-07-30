using SQLite;
using System;

namespace AlexPacientes.Models.SQLite
{
    [Table("SessionChatData")]
    public class SessionChatData
    {
        [PrimaryKey, NotNull, AutoIncrement]
        public int PK { get; set; }
        public int id { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public int appointmentId { get; set; }
        public int userId { get; set; }
        public int doctorId { get; set; }
        public string session { get; set; }
        public string user_token { get; set; }
        public string doctor_token { get; set; }


        [Ignore]
        public DateTime ExpirationDate {
            get
            {
                var dt = new Converters.LongToDatetimeConverter();
                return ((DateTime)dt.Convert(this.created_at, typeof(DateTime), null, System.Globalization.CultureInfo.InvariantCulture)).AddDays(5);
            }
        }

        [Ignore]
        public NewApiModels.AppointmentStatus.Item SessionStatus
        {
            get {
                return new NewApiModels.AppointmentStatus.Item()
                {
                    appointmentId=appointmentId,
                    created_at=created_at,
                    doctorId=doctorId,
                    doctor_token=doctor_token,
                    id=id,
                    session=session,
                    updated_at=updated_at,
                    userId=userId,
                    user_token=user_token
                };
            }

            set
            {
                id = value.id;
                created_at = value.created_at;
                updated_at = value.updated_at;
                appointmentId = value.appointmentId;
                userId = value.userId;
                doctorId = value.doctorId;
                session = value.session;
                user_token = value.user_token;
                doctor_token = value.doctor_token;
            }
        }
    }
}
