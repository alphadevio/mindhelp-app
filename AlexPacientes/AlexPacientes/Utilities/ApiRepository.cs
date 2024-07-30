using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AlexPacientes.Utilities
{
    public class ApiRepository
    {
        protected RestClient client;

        public ApiRepository()
        {
            client = new RestClient();
        }

        /// <summary>
        /// Request to Sign In endpoint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<SignInResponseModel> SignIn(Models.NewApiModels.LoginRequest data)
        {
            try
            {
                var response = await client.PostAsync<SignInResponseModel>(ApiSettings.Methods.LOGIN, data);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Request to Sign Up endpoint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<SignUpResponseModel> SignUp(Models.NewApiModels.SignupModelRequest data)
        {
            try
            {
                var response = await client.PostAsync<SignUpResponseModel>(ApiSettings.Methods.REGISTER, data);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Request to Reset Password Request endpoint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ForgotPasswordResponseModel> ForgotPassword(Models.NewApiModels.RecoverPasswordRequest data)
        {
            try
            {
                var response = await client.PostAsync<ForgotPasswordResponseModel>(ApiSettings.Methods.FORGOT_PASSWORD, data);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Request to Edit User endpoint
        /// </summary>
        /// <param name="data">Models.NewApiModels.EditProfile</param>
        /// <returns></returns>
        public async Task<EditUserResponseModel> EditUser(object data, int userID)
        {
            try
            {
                var response = await client.PatchAsync<EditUserResponseModel>(string.Format(ApiSettings.Methods.UPDATE_PROFILE, userID), data, GlobalConfig.Identity.Enckey);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Request for the categories MindHelp has
        /// </summary>
        /// <returns></returns>
        public async Task<CategoriesResponseModel> Categories()
        {
            try
            {
                var response = await client.GetAsync<CategoriesResponseModel>(ApiSettings.Methods.GET_CATEGORIES);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Get doctors with advanced filters
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public async Task<DoctorsResponseModel> DoctorsByCategoryAdvancedFilters(int categoryID/*, DateTime startDate, DateTime endDate, string searchTerm, string gender, long userId*/)
        {
            try
            {
                DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var time = (long)(DateTime.Now.ToUniversalTime() - baseDate).TotalMilliseconds;
                string format = string.Format(ApiSettings.Methods.GET_DOCTORS_BY_CATEGORY_ADVANCED_FILTERS, categoryID, time);
                var response = await client.GetAsync<DoctorsResponseModel>(format);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }


        public async Task<DoctorsResponseModel> DoctorsByCategoryAdvancedFilters(int categoryID, DateTime startDate, DateTime endDate, TimeSpan startHour, TimeSpan endHour, string searchTerm, string gender, long userId)
        {
            try
            {
                string filters = string.Empty;

                startDate=startDate.AddHours(startHour.Hours);
                startDate=startDate.AddMinutes(startHour.Minutes);
                filters += $@"&startDate={startDate.ToString("yyyy-MM-dd HH:mm:ss")}";

                endDate=endDate.AddHours(endHour.Hours);
                endDate=endDate.AddMinutes(endHour.Minutes);
                filters += $@"&endDate={endDate.ToString("yyyy-MM-dd HH:mm:ss")}";

                if(!string.IsNullOrWhiteSpace(searchTerm))
                    filters += $@"&searchTerm={searchTerm}";

                if(!string.IsNullOrWhiteSpace(gender))
                    filters += $@"&gender={gender}";

                if(userId!=0)
                    filters += $@"&userId={userId}";
                DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var time = (long)(DateTime.Now.ToUniversalTime() - baseDate).TotalMilliseconds;

                var response = await client.GetAsync<DoctorsResponseModel>(string.Format(ApiSettings.Methods.GET_DOCTORS_BY_CATEGORY_ADVANCED_FILTERS, categoryID, time)+filters);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }


        /// <summary>
        /// Request for the doctors from a category
        /// </summary>
        /// <returns></returns>
        public async Task<DoctorsResponseModel> Doctors(int categoryID)
        {
            try
            {
                var response = await client.GetAsync<DoctorsResponseModel>(string.Format(ApiSettings.Methods.GET_DOCTORS, categoryID));
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Request for the reviews of a doctor
        /// </summary>
        /// <returns></returns>
        public async Task<ReviewsResponseModel> Reviews(int doctorID)
        {
            try
            {
                var response = await client.GetAsync<ReviewsResponseModel>(string.Format(ApiSettings.Methods.GET_REVIEWS, doctorID));
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Request for the time slots of a doctor
        /// </summary>
        /// <returns></returns>
        public async Task<TimeSlotsResponseModel> TimeSlots(int doctorID, DateTime timeAsReference)
        {
            try
            {
                string query = string.Format("?time_as_reference={0}", new DateTimeOffset(timeAsReference).ToUnixTimeMilliseconds());
                var response = await client.GetAsync<TimeSlotsResponseModel>(string.Format(ApiSettings.Methods.GET_DOCTOR_TIMES, doctorID) + query);

                var timeConverter = new Converters.LongToDatetimeConverter();
                var newCalendar = new AlexPacientes.Models.NewApiModels.Responses.TimeSlotListModel();
                newCalendar.Items = new List<TimeSlotModel>();
                //Creamos la agenda 
                for(int x=0;x<DateTime.DaysInMonth(timeAsReference.Year,timeAsReference.Month);x++)
                {
                    var date = new DateTime(timeAsReference.Year, timeAsReference.Month, x + 1,0,0,0);
                    newCalendar.Items.Add(new TimeSlotModel()
                    {
                        Day = new DateTimeOffset(date).ToUnixTimeMilliseconds(),
                        Availability = new List<AvailabilityTimeSlotModel>()
                    });
                }

                //Recorre los dias del calendario que viene del server y obtiene dias
                foreach(var day in response.Data.Items)
                {
                    var timeReferenceDay = (DateTime)timeConverter.Convert(day.Day, null, null, null);
                    var difHour = (DateTime.Now - DateTime.UtcNow).Hours;

                    //Recorre el dia del calendario y obtiene las horas
                    foreach(var hour in day.Availability)
                    {
                        if (hour.slot_type == "availability")
                        {
                            //Debe de venir de timereferenceday, iniciando horas en 0, con suma de cuando empieza mas la diferencia gmt, agregar los minutos de ser necesario
                            DateTime realLocalTimeHourStart, realLocalTimeHourEnd;
                            if (hour.StartAt + difHour < 0 || hour.StartAt+difHour>23)
                            {
                                var remainingHours = (hour.StartAt + difHour) % 24;
                                realLocalTimeHourStart = new DateTime(timeReferenceDay.Year, timeReferenceDay.Month, timeReferenceDay.Day, 0, 0, 0);
                                realLocalTimeHourStart=realLocalTimeHourStart.Add(new TimeSpan(0, remainingHours, hour.minutes, 0));
                            }
                            else
                                realLocalTimeHourStart = new DateTime(timeReferenceDay.Year, timeReferenceDay.Month, timeReferenceDay.Day, hour.StartAt + difHour, hour.minutes, 0);
                            if (hour.EndAt + difHour < 0 || hour.EndAt + difHour > 23)
                            {
                                var remainingHours = (hour.EndAt + difHour) % 24;
                                realLocalTimeHourEnd = new DateTime(timeReferenceDay.Year, timeReferenceDay.Month, timeReferenceDay.Day, 0, 0, 0);
                                realLocalTimeHourEnd = realLocalTimeHourEnd.Add(new TimeSpan(0, remainingHours, hour.minutes, 0));
                            }
                            else
                                realLocalTimeHourEnd = new DateTime(timeReferenceDay.Year, timeReferenceDay.Month, timeReferenceDay.Day, hour.EndAt + difHour, hour.minutes, 0);

                            var match = newCalendar.Items.FirstOrDefault(e =>
                                          ((DateTime)timeConverter.Convert(e.Day, null, null, null)).Day == realLocalTimeHourStart.Day
                                          && ((DateTime)timeConverter.Convert(e.Day, null, null, null)).Month == realLocalTimeHourStart.Month);
                            if (match != null)
                                match.Availability.Add(new AvailabilityTimeSlotModel()
                                {
                                    Available = hour.Available,
                                    StartAt = realLocalTimeHourStart.Hour,
                                    EndAt = realLocalTimeHourEnd.Hour,
                                    Day = realLocalTimeHourStart.DayOfWeek.ToString(),
                                    ID = hour.ID,
                                    minutes = hour.minutes,
                                    slot_type = hour.slot_type
                                });
                        }
                    }
                }

                response.Data = newCalendar;
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Request to Save Appointment endpoint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ResponseBaseModel<object>> SaveAppointment(Models.NewApiModels.SaveAppointmentRequest data)
        {
            try
            {
                var response = await client.PostAsync<ResponseBaseModel<object>>(ApiSettings.Methods.SAVE_APPOINTMENT, data, GlobalConfig.Identity.Enckey);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        /// <summary>
        /// Get customer payment cards
        /// </summary>
        /// <param name="userID">customer ID</param>
        /// <returns></returns>
        public async Task<List<Models.NewApiModels.Responses.CreditCard>>CreditCards(int userID)
        {
            try
            {
                var response = await client.GetAsync<CreditCardResponse>(string.Format(ApiSettings.Methods.GET_CREDIT_CARD, userID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch(Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return new List<CreditCard>();
        }

        public async Task<bool> AddCreditCard(int userID, Models.AppModels.Payments.PaymentCardModel Card)
        {
            try
            {
                var conektaKey = (await client.GetAsync<PaymentKeyResponseModel>(ApiSettings.Methods.GET_PAYMENT_KEYS, GlobalConfig.Identity.Enckey)).Data.Items[0];
                string token = "";
                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                    token = await new Conekta.Xamarin.ConektaTokenizer(conektaKey.Key, Conekta.Xamarin.RuntimePlatform.Android).GetTokenAsync(Card.CardNumber, Card.AccountHolder, Card.CVC, Card.Year, Card.Month);
                else
                    token = await new Conekta.Xamarin.ConektaTokenizer(conektaKey.Key, Conekta.Xamarin.RuntimePlatform.iOS).GetTokenAsync(Card.CardNumber, Card.AccountHolder, Card.CVC, Card.Year, Card.Month);

                var data = new { card_token = token };
                var response = await client.PostAsync<CreditCardResponse>(string.Format(ApiSettings.Methods.POST_CREDIT_CARD, userID), data, GlobalConfig.Identity.Enckey);
                return response.HasValidStatus();
            }
            catch { return false; }
        }

        public async Task<bool>DeleteCreditCard(int userID, int cardIndex)
        {
            try
            {
                var response = await client.DeleteAsync<CreditCardResponse>(string.Format(ApiSettings.Methods.DELETE_CREDIT_CARD, userID, cardIndex), GlobalConfig.Identity.Enckey);
                return response.HasValidStatus();
            }
            catch { }
            return false;
        }

        public async Task<bool> SetDefaultCreditCard(int userID, int cardIndex)
        {
            try
            {
                var response = await client.PostAsync<SetDefaultCreditCardResponseModel>(string.Format(ApiSettings.Methods.POST_SET_DEFAULT_CREDIT_CARD, userID, cardIndex), null, GlobalConfig.Identity.Enckey);
                return response.HasValidStatus();
            }
            catch { return false; }
        }


        /// <summary>
        /// Request to validate promo code endpoint
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<PromoCodeResponseModel> VerifyPromoCode(Models.NewApiModels.VerifyPromoCodeRequest data)
        {
            try
            {
                var response = await client.PostAsync<PromoCodeResponseModel>(ApiSettings.Methods.APPLY_COUPON, data, GlobalConfig.Identity.Enckey);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        public async Task<List<Models.NewApiModels.Responses.Appointment>> GetCurrentAppointments(int userID)
        {
            var appointments= new List<Appointment>(); 
            try
            {
                var allAppointments = await GetNextAppointments(userID);
                var currentAppointments = await GetInProgressAppointments(userID);
                var converter = new AlexPacientes.Converters.LongToDatetimeConverter();
                allAppointments.ForEach(e =>
                {
                    var meetingDate = (DateTime)converter.Convert(e.StartAt, null, null, null);
                    if (DateTime.UtcNow.AddHours(-4) < meetingDate && DateTime.UtcNow.AddHours(4) > meetingDate)
                        appointments.Add(e);
                });
                currentAppointments.ForEach(e =>
                {
                    var meetingDate = (DateTime)converter.Convert(e.StartAt, null, null, null);
                    if (DateTime.UtcNow.AddHours(-12) < meetingDate && DateTime.UtcNow.AddHours(12) > meetingDate)
                        appointments.Add(e);
                });
            }
            catch { }
            return appointments;
        }

        public async Task<List<Models.NewApiModels.Responses.Appointment>>GetFutureAppointments(int userID)
        {
            var pendingAccepntanceAppointments = GetPendingToConfirmAppointments(userID);
            var nextAppointments = GetNextAppointments(userID);

            await Task.WhenAll(new Task[] { pendingAccepntanceAppointments, nextAppointments });
            pendingAccepntanceAppointments.Result.AddRange(nextAppointments.Result.AsEnumerable());
            return pendingAccepntanceAppointments.Result;
        }

        public async Task<List<Models.NewApiModels.Responses.Appointment>> GetNextAppointments(int userID)
        {
            try
            {
                var response = await client.GetAsync<Models.NewApiModels.Responses.Appointments>(string.Format(ApiSettings.Methods.GET_ALL_NEXT_APPOINTMENTS, userID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<Appointment>();
        }

        public async Task<List<Models.NewApiModels.Responses.Appointment>>GetInProgressAppointments(int userID)
        {
            try
            {
                var response = await client.GetAsync<Models.NewApiModels.Responses.Appointments>(string.Format(ApiSettings.Methods.GET_ALL_CURRENT_APPOINTMENTS, userID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<Appointment>();
        }

        public async Task<List<Models.NewApiModels.Responses.Appointment>>GetPastAppointments(int userID)
        {
            try
            {
                var response = await client.GetAsync<Models.NewApiModels.Responses.Appointments>(string.Format(ApiSettings.Methods.GET_PAST_APPOINTMENTS, userID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<Appointment>();
        }

        public async Task<List<Models.NewApiModels.Responses.Appointment>> GetPendingToConfirmAppointments(int userID)
        {
            try
            {
                var response = await client.GetAsync<Models.NewApiModels.Responses.Appointments>(string.Format(ApiSettings.Methods.GET_PENDING_TO_CONFIRM_APPOINTMENTS, userID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<Appointment>();
        }

        public async Task<AlexPacientes.Models.NewApiModels.Responses.Session>GetSessionAppointmentData(int userID, int appointmentID)
        {
            try
            {
                var data = new
                {
                    user = userID,
                    appointment = appointmentID
                };
                var response = await client.PostAsync<AlexPacientes.Models.NewApiModels.Responses.SessionResponseModel>(string.Format(ApiSettings.Methods.POST_GET_CONNECTION_DATA, userID, appointmentID), data, GlobalConfig.Identity.Enckey);
                return response.Data.Items.FirstOrDefault();
            }
            catch {  }
            return null;
        }
        
        public async Task<bool> FinishAppointment(int userID, int appointmentID)
        {
            try
            {
                var data = new
                {
                    user = userID,
                    appointment = appointmentID
                };
                var response = await client.PostAsync<AlexPacientes.Models.NewApiModels.Responses.SessionResponseModel>(string.Format(ApiSettings.Methods.POST_FINISH_MEETING, userID, appointmentID), data, GlobalConfig.Identity.Enckey);
                return response!=null;
            }
            catch { }
            return false;
        }

        public async Task<bool> RateSession(int sessionID, double rate, string commentaries)
        {
            try
            {
                var data = new { message = string.IsNullOrWhiteSpace(commentaries)?" ": commentaries, score=rate };
                var response = await client.PostAsync<RateResponseModel>(string.Format(ApiSettings.Methods.POST_RATE_APPOINTMENT, sessionID), data, GlobalConfig.Identity.Enckey);
                return response.HasValidStatus();
            }
            catch { return false; }
            
        }
    
        public async Task<List<AlexPacientes.Models.NewApiModels.Responses.ChatRoom>>GetChatRooms(int userID)
        {
            try
            {
                var response = await client.GetAsync<AlexPacientes.Models.NewApiModels.Responses.ChatRoomResponseModel>(string.Format(ApiSettings.Methods.GET_CHATROOMS, userID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<ChatRoom>();
        }

        public async Task<ImageMediaSource> GetProfileImage(long imageSourceID)
        {
            try
            {
                var response = await client.GetAsync<AlexPacientes.Models.NewApiModels.Responses.ImageMediaSourceResponseModel>(string.Format(ApiSettings.Methods.GET_IMAGE_SOURCE_FROM_ID, imageSourceID), GlobalConfig.Identity.Enckey);
                response.Data.Items[0].Source = response.Data.Items[0].Url;
                return response.Data.Items[0];//force a crash if the list does not have items
            }
            catch { }
            return new ImageMediaSource()
            {
                Source = Styles.Icons.MindPlaceholder
            };
        }
    
        public async Task<bool> PostNotificationToken(int userID, string token)
        {
            try
            {
                var data = new Models.NewApiModels.PostTokenRequest()
                {
                    Token = token
                };
                var response = await client.PostAsync<Models.NewApiModels.Responses.NotificationTokenResponseModel>(string.Format(ApiSettings.Methods.POST_SEND_NOTIFICATION_TOKEN, userID), data, GlobalConfig.Identity.Enckey);
                return response.HasValidStatus();
            }
            catch { return false; }
        }
        
        public async Task<bool> PostChatMessage(int userID, int chatID, string message)//update to support image sources (bit array/base64)
        {
            try
            {
                var data = new Models.NewApiModels.PostMessageRequest() { message = message };
                var response = await client.PostAsync<CreditCardResponse>(string.Format(ApiSettings.Methods.POST_SEND_CHAT_MESSAGE, userID, chatID), data, GlobalConfig.Identity.Enckey);
                return response.HasValidStatus();
            }
            catch { return false; }
        }

        public async Task<List<MessageModel>>GetChatMessagesHistory(int userID, int chatRoomID)
        {
            try
            {
                var response = await client.GetAsync<ChatMessagesResponseModel>(string.Format(ApiSettings.Methods.GET_CHAT_MESSAGES_HISTORY, userID, chatRoomID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { return new List<MessageModel>(); }            
        }

        /// <summary>
        /// Request for contact us
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBaseModel<object>> SendContactForm(Models.NewApiModels.SendContactFormRequest data)
        {
            try
            {
                var response = await client.PostAsync<ResponseBaseModel<object>>(ApiSettings.Methods.POST_CONTACT_FORM, data, GlobalConfig.Identity.Enckey);
                return response;
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
            }
            return null;
        }

        public async Task<bool> SendMessage(string Message, string therapistOSToken, int appointmentID, int doctorID)
        {
            try
            {
                var push = new Models.AppModels.PushNotifications.RequestPushModel()
                {
                    include_player_ids = new List<string>()
                    {
                        GlobalConfig.USER_PUSH_NOTIFICATION_ID,therapistOSToken
                    },
                    data = new Models.AppModels.PushNotifications.Data()
                    {
                        Title = Labels.Labels.NewMessageReceived,
                        IsSilentNotification = false,
                        message = Message,
                        messageOwnerID = GlobalConfig.Identity.ID,
                        messageOwnerName = GlobalConfig.Identity.FirstName,
                        sendTime = DateTime.Now,
                        Subtitle = Message
                    },
                };
                var message = new Models.ApiModels.NewApiRequests.ChatAPIMessageRequest()
                {
                    appointment_id = appointmentID,
                    doctor_user_id = doctorID,
                    message = Message,
                    patient_user_id = GlobalConfig.Identity.ID,
                    sent_by_user_id = GlobalConfig.Identity.ID
                };
                var response = await client.PostAsync<Models.ApiModels.NewApiResponses.Messages.Message>(ApiSettings.Methods.POST_CHAT_MESSAGE, message, GlobalConfig.Identity.Enckey);
                if (response == null || response.data==null||response.data.items.Count==0) return false;
                //await App.PushNotificationHandler.PostNotification(push);
                return true;
            }
            catch { }
            return false;
        }

        public async Task<bool> SendNotificationToDoctor(string doctorNotificationToken, string message)
        {
            try
            {
                var push = new Models.AppModels.PushNotifications.RequestPushModel()
                {
                    include_player_ids = new List<string>()
                    {
                        doctorNotificationToken
                    },
                    data = new Models.AppModels.PushNotifications.Data()
                    {
                        Title = Labels.Labels.NewMessageReceived,
                        IsSilentNotification = false,
                        message = message,
                        messageOwnerID = GlobalConfig.Identity.ID,
                        messageOwnerName = GlobalConfig.Identity.FirstName,
                        sendTime = DateTime.Now,
                        Subtitle = message
                    },
                };
                //await App.PushNotificationHandler.PostNotification(push, sendToSender: false);
                return true;
            }
            catch { }
            return false;
        }

        public async Task<string> GetChatSessionData(int doctorID)
        {
            try
            {
                var response = await client.GetAsync<Models.NewApiModels.GetNotificationTokens.DoctorNotificationToken.GetDoctorNotificationToken>(string.Format(ApiSettings.Methods.GET_DOCTOR_NOTIFICATION_TOKEN, doctorID), GlobalConfig.Identity.Enckey);
                if (response?.data?.items?.Count > 0 && response.data.items[0].id != 0)
                    return response.data.items[0].token;
            }
            catch { }
            return string.Empty;
        }

        public async Task<string> GetDoctorToken(int doctorID)
        {
            try
            {
                var client = new RestClient();
                var response = await client.GetAsync<Models.NewApiModels.GetNotificationTokens.DoctorNotificationToken.GetDoctorNotificationToken>(string.Format(ApiSettings.Methods.GET_DOCTOR_NOTIFICATION_TOKEN, doctorID), GlobalConfig.Identity.Enckey);
                if (response?.data?.items?.Count > 0 && response.data.items[0].id != 0)
                    return response.data.items[0].token;
            }
            catch { }
            return string.Empty;
        }

        public async Task<List<Models.NewApiModels.Plan>> GetPlans()
        {
            try
            {
                var response = await client.GetAsync<PlansResponseModel>(ApiSettings.Methods.GET_PLANS, GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<Models.NewApiModels.Plan>();
        }

        public async Task<List<Models.NewApiModels.Subscription<Models.NewApiModels.Plan, Models.AppModels.Identity>>> SetSubscription(long planId)
        {
            try
            {
                var response = await client.PostAsync<SetSubscriptionResponseModel>(string.Format(ApiSettings.Methods.POST_SUBSCRIPTION, GlobalConfig.Identity.ID, planId), null, GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<Models.NewApiModels.Subscription<Models.NewApiModels.Plan, Models.AppModels.Identity>>();
        }

        public async Task<List<Models.NewApiModels.Subscription<Models.NewApiModels.Plan, Models.AppModels.Identity>>> GetActiveSubscription()
        {
            try
            {
                var response = await client.GetAsync<ActiveSubscriptionResponseModel>(string.Format(ApiSettings.Methods.GET_ACTIVE_SUBSCRIPTION, GlobalConfig.Identity.ID), GlobalConfig.Identity.Enckey);
                return response.Data.Items;
            }
            catch { }
            return new List<Models.NewApiModels.Subscription<Models.NewApiModels.Plan, Models.AppModels.Identity>>();
        }

        public async Task<bool> RescheduleAppointment(int appointmentID, Models.NewApiModels.SendRescheduleAppointmentRequest request)
        {
            try
            {
                var response = await client.PostAsync<ResponseBaseModel<object>>(string.Format(ApiSettings.Methods.POST_RESCHEDULE_APPOINTMENT, appointmentID), request, GlobalConfig.Identity.Enckey);
                return response.Status==ApiSettings.Status.SUCCESS;
            }
            catch { }
            return false;
        }
        public async Task<bool> CancelSubscriptio(int userID)
        {
            try
            {
                var response = await client.DeleteAsync<ResponseBaseModel<object>>(string.Format(ApiSettings.Methods.DELETE_CANCEL_SUBSCRIPTION, userID), GlobalConfig.Identity.Enckey);
                return response.Status == ApiSettings.Status.SUCCESS;
            }
            catch { }
            return false;
        }
        public async Task<bool> DeleteAccount(int userID)
        {
            try
            {
                var response = await client.DeleteAsync<ResponseBaseModel<object>>(string.Format(ApiSettings.Methods.DELETE_DELETE_ACCOUNT, userID), GlobalConfig.Identity.Enckey);
                return response.Status == ApiSettings.Status.SUCCESS;
            }
            catch { }
            return false;
        }
    }
}
