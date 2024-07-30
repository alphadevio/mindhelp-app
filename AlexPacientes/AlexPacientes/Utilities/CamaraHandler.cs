using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using AlexPacientes.Models.AppModels.Camera;
using System;

namespace AlexPacientes.Utilities
{
    public class CamaraHandler
    {
        //Primero se debe de preguntar si se puede tomar la foto!
        public string CurrentFilePath = "";
        public ImageSource Source;
        public byte[] imageArray;
        public string Name = "";
        public string Type = "jpg";
        public int DefaultCompresionQuality { get; set; } = 50;
        public Plugin.Media.Abstractions.PhotoSize DefaultPhotoSize { get; set; } = Plugin.Media.Abstractions.PhotoSize.Medium;
        public CamaraHandler()
        {
            Inicialize();
        }

        public async Task Inicialize()
        {
            await CrossMedia.Current.Initialize();
        }

        public bool CanTakePhoto()
        {
            return (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) ? false : true;
        }

        public bool CanTakeVideo()
        {
            return (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported) ? false : true;
        }

        public bool CanPickPhoto()
        {
            return CrossMedia.Current.IsPickPhotoSupported;
        }

        public bool CanPickVideo()
        {
            return CrossMedia.Current.IsPickVideoSupported;
        }

        #region BaseMethods
        async Task TakePhoto()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "pic.jpg",
                CompressionQuality = DefaultCompresionQuality,
                PhotoSize = DefaultPhotoSize,
                SaveToAlbum = true
            });

            if (file == null)
            {
                Source = null;
                return;
            }
            CurrentFilePath = file.Path;
            string[] PathArray;
            if (string.IsNullOrEmpty(file.AlbumPath))
                PathArray = file.Path.Split('/');
            else
                PathArray = file.AlbumPath.Split('/');
            Name = PathArray[PathArray.Length - 1];
            var nameParths = Name.Split('.');
            this.Type = nameParths.Length > 1 ? nameParths[nameParths.Length - 1] : "jpg";

            imageArray = ReadFully(file.GetStream());
            Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
        async Task PickPhoto()
        {
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = DefaultPhotoSize, CompressionQuality = DefaultCompresionQuality });
            if (file == null)
            {
                Source = null;
                return;
            }

            CurrentFilePath = file.Path;
            var PathArray = file.AlbumPath.Split('/');
            Name = PathArray[PathArray.Length - 1];
            var nameParths = Name.Split('.');
            this.Type = nameParths.Length > 1 ? nameParths[nameParths.Length - 1] : "jpg";


            imageArray = ReadFully(file.GetStream());

            Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }
        async Task PickVideo()
        {
            var file = await CrossMedia.Current.PickVideoAsync();
            if (file == null)
            {
                Source = null;
                return;
            }

            CurrentFilePath = file.Path;
            var PathArray = CurrentFilePath.Split('/');
            Name = PathArray[PathArray.Length - 1];
            var nameParths = Name.Split('.');
            this.Type = nameParths.Length > 1 ? nameParths[nameParths.Length - 1] : "mp4";


            imageArray = ReadFully(file.GetStream());

        }
        #endregion

        #region Public full implementations
        public async Task<CameraDataModel> PickPhotoAsync()
        {
            try
            {
                if (!this.CanTakePhoto())
                {
                    await App.Current.MainPage.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.LibraryNotAvailable, Labels.Labels.OK);
                    return null;
                }
                await this.PickPhoto();
                if (this.Source == null)
                    return null;
                var BaseImageSource = new CameraDataModel()
                {
                    Path = this.CurrentFilePath,
                    Source = this.Source,
                    Name = this.Name,
                    byteSource = this.imageArray,
                    Type = this.Type
                };
                return BaseImageSource;
            }
            catch (Exception exc)
            {
                var cad = exc.ToString();
            }
            return null;
        }
        public async Task<CameraDataModel> TakePhotoAsynk()
        {
            try
            {
                if (!this.CanTakePhoto())
                {
                    await App.Current.MainPage.DisplayAlert(Labels.Labels.ERROR, Labels.Labels.NotCameraAvailable, Labels.Labels.OK);
                    return null;
                }
                await this.TakePhoto();
                if (this.Source == null)
                    return null;
                var BaseImageSource = new CameraDataModel()
                {
                    Path = this.CurrentFilePath,
                    Source = this.Source,
                    Name = this.Name,
                    byteSource = this.imageArray,
                    Type = this.Type
                };
                return BaseImageSource;
            }
            catch (Exception exc)
            {
                var cad = exc.ToString();
            }
            return null;
        }
        #endregion

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }


        
    }
}
