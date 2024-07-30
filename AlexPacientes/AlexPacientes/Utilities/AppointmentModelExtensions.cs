
using AlexPacientes.Styles;

namespace AlexPacientes.Utilities
{
    public static class AppointmentModelExtensions
    {
        public static Models.ApiModels.UserDoctorModel ConvertToUserDoctor(this Models.NewApiModels.Responses.Appointment appointment)
        {
            return new Models.ApiModels.UserDoctorModel
            {
                CategoryID = 0,
                ID = (int)appointment.DoctorUserId.Id,
                FirstName = appointment.DoctorUserId.FirstName,
                LastName = appointment.DoctorUserId.LastName,
                Name = string.Format("{0} {1}", appointment.DoctorUserId.FirstName, appointment.DoctorUserId.LastName),
                ProfileImage = Icons.MindPlaceholder,
                ConsultationFee = appointment.FinalPrice,
                Doctor = new Models.ApiModels.DoctorModel
                {
                    Speciality = "Terapeuta",
                    SpecialityName = "Terapeuta",
                    CategoryName = "Terapeuta",
                    ConsultationFee = appointment.FinalPrice
                }
            };
        }
    }
}
