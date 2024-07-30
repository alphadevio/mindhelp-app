using AlexPacientes.Styles;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Models.AppModels.Chat
{
    public class PendingChatModel
    {
        public NewApiModels.Responses.ChatRoom ChatDataContext { get; set; }
        public ImageSource UserImage { get; set; }
        //public bool IsPasient { get; set; }
        public string StartDate { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }

        //public void SetDemoData(bool isDoctor)
        //{
        //    if (isDoctor)
        //    {
        //        UserImage = Icons.TestDoctor;
        //        IsPasient = false;
        //    }
        //    else
        //    {
        //        UserImage = Icons.TestClient;
        //        IsPasient = true;
        //    }
        //    StartDate = DateTime.Now;
        //}
    }
}
