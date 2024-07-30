using AlexPacientes.Models.ApiModels;
using AlexPacientes.Models.AppModels;
using AlexPacientes.Models.NewApiModels;
using AlexPacientes.Settings;
using AlexPacientes.Utilities;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Transfer;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.ViewModels.Settings
{
    public class EditProfileViewModel : ViewModelBase
    {
        private Profile _profileData;
        public Profile ProfileData
        {
            get { return _profileData; }
            set { _profileData = value;
                OnPropertyChanged();
            }
        }

        public Command SaveCommand { get; set; }
        public Command ChangePhotoCommand { get; set; }

        private bool _imageChanged;

        public EditProfileViewModel(Page context) : base(context)
        {
            SaveCommand = new Command(async () => await SaveProfile());
            ChangePhotoCommand = new Command(async () => await ChangePhoto());

            SetProfileData();
        }

        private void SetProfileData()
        {
            try
            {
                ProfileData = new Profile
                {
                    ProfileImage = GlobalConfig.Identity.ProfileImage,
                    FirstName = GlobalConfig.Identity.FirstName,
                    LastName = GlobalConfig.Identity.LastName,
                    Email = GlobalConfig.Identity.Email,
                    Country = GlobalConfig.Identity.Country,
                    CountryCode = GlobalConfig.Identity.CountryCode,
                    PhoneNumber = GlobalConfig.Identity.Phone,
                    Birthday = GlobalConfig.Identity.Birthday,
                    Gender = GlobalConfig.Identity.Gender,
                    BirthdayDateTime = DateTimeOffset.FromUnixTimeMilliseconds(GlobalConfig.Identity.Birthday).DateTime,
                    ProfileImageSource = GlobalConfig.Identity.ProfileImage
                };
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                DisplayError(Labels.Labels.GenericErrorMessage);
            }
        }

        private async Task SaveProfile()
        {
            IsBusy = true;
            try
            {
                ProfileData.CountryCode = "+52";
                ProfileData.Birthday = new DateTimeOffset(ProfileData.BirthdayDateTime).ToUnixTimeMilliseconds();
                var profileData = new Models.NewApiModels.EditProfile()
                {
                    first_name = ProfileData.FirstName,
                    last_name = ProfileData.LastName,
                    phone = ProfileData.PhoneNumber,
                    birth_at = ProfileData.Birthday.ToString(),
                    gender = ProfileData.Gender,
                    country = ProfileData.Country,
                    state = "",
                    city = "",
                    street_address = "",
                    zip = "",
                    country_code = ProfileData.CountryCode,
                    country_code_flag = "",
                    time_zone = ProfileData.TimeZone
                };
                Models.NewApiModels.Responses.EditUserResponseModel response;
                if (_imageChanged)
                {
                    var profileDataWImage = new Models.NewApiModels.EditProfileExtention()
                    {
                        base64 = ProfileData.ProfileImageBase64,
                        base64_extension = ProfileData.ProfileImageExtension
                    };
                    profileDataWImage.setValues(profileData);
                    response = await new ApiRepository().EditUser(profileDataWImage, GlobalConfig.Identity.ID);
                }
                else
                    response = await new ApiRepository().EditUser(profileData, GlobalConfig.Identity.ID);
                if (response.HasValidStatus())
                {
                    GlobalConfig.Identity.FirstName = ProfileData.FirstName;
                    GlobalConfig.Identity.LastName = ProfileData.LastName;
                    GlobalConfig.Identity.Country = ProfileData.Country;
                    GlobalConfig.Identity.Phone = ProfileData.PhoneNumber;
                    GlobalConfig.Identity.Birthday = ProfileData.Birthday;
                    GlobalConfig.Identity.Gender = ProfileData.Gender;

                    // Get the image
                    if (_imageChanged)
                    {
                        var media = (PhotoMedia) await new ApiRepository().GetProfileImage(response.Data.Items[0].PhotoMediaID);
                        GlobalConfig.Identity.PhotoMedia = media;
                    }

                    MessagingCenter.Send(this, "RefreshIdentity");
                    await Alert.DisplayAlert(Labels.Labels.Done, Labels.Labels.ProfileDataUpdated, Labels.Labels.OK);
                }
                else
                {
                    await DisplayError(response.Error.Errors[0].Message);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                await DisplayError(Labels.Labels.GenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ChangePhoto()
        {
            Plugin.Media.Abstractions.MediaFile file = null;
            var option = await Alert.DisplayActionSheet("", Labels.Labels.Cancel, null, new string[] { Labels.Labels.Camera, Labels.Labels.Gallery });
            if(option == Labels.Labels.Camera)
            {
                file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 1024
                });               
            }
            else if(option == Labels.Labels.Gallery)
            {
                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 1024
                });
            }
            else
            { 
                return;
            }

            if (file == null) return;

            // Convert image to base64
            ProfileData.ProfileImageBase64 = await GlobalUtilities.ConvertToBase64(file.GetStream());
            ProfileData.ProfileImageExtension = file.FileExtension().Substring(1);

            // Display image
            ProfileData.ProfileImageSource = ImageSource.FromStream(() => file.GetStream());
            OnPropertyChanged("ProfileData");
            _imageChanged = true;

            //// Deploy image to AWS S3
            //var url = await UploadPhoto(file);
            //if (url != null)
            //    ProfileData.ProfileImage = url;
        }

        private async Task<string> UploadPhoto(Plugin.Media.Abstractions.MediaFile file)
        {
            try
            {
                // Configure AWS
                AWSConfigs.AWSRegion = "us-west-1";
                AWSConfigsS3.UseSignatureVersion4 = true;
                AWSConfigs.CorrectForClockSkew = true;

                // Create credentials and client
                var credentials = new CognitoAWSCredentials("us-east-1:6ae17c43-fa27-467c-ae6b-4d67b1ef9d3c", RegionEndpoint.USEast1);
                var s3Client = new AmazonS3Client(credentials, RegionEndpoint.USWest1);

                // File info
                var fileName = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var fileExt = file.FileExtension();
                var fileKey = $"uploads/Paciente_{fileName}{fileExt}";

                // Create utility and upload file
                var transferUtility = new TransferUtility(s3Client);
                await transferUtility.UploadAsync(file.Path, "mind-help", fileKey);
                return string.Format("{0}{1}","https://mind-help.s3.us-west-1.amazonaws.com/", fileKey);
            }   
            catch(Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return null;
            }
        }

        public class Profile
        {
            public string ProfileImage { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string CountryCode { get; set; }
            public string Country { get; set; }
            public string PhoneNumber { get; set; }
            [Newtonsoft.Json.JsonProperty("birth_at")]
            public long Birthday { get; set; }
            public string Gender { get; set; }
            public string TimeZone { get; set; } = TimeZoneInfo.Local.Id;
            [Newtonsoft.Json.JsonIgnore]
            public DateTime BirthdayDateTime { get; set; }
            [Newtonsoft.Json.JsonIgnore]
            public ImageSource ProfileImageSource { get; set; }
            public string ProfileImageExtension { get; set; }
            public string ProfileImageBase64 { get; set; }
        }
    }
}
