using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Models.AppModels.Camera
{
    public class CameraDataModel
    {
        public ImageSource Source { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public byte[] byteSource { get; set; }
        public string Type { get; set; }
        public void Clear()
        {
            Source = null;
            Path = string.Empty;
            Name = string.Empty;
            Type = string.Empty;
            byteSource = null;
        }
    }
}
