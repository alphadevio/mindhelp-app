using AlexPacientes.Models.AppModels.Chat;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexPacientes.Views.Chat.Cells
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public static readonly DataTemplate PacientDataTemplate = new DataTemplate(typeof(PacientChatCell));
        public static readonly DataTemplate DoctorDataTemplate = new DataTemplate(typeof(DoctorChatCell));
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as Models.NewApiModels.Responses.MessageModel;
            if (message.IsPasient)
                return PacientDataTemplate;
            else
                return DoctorDataTemplate;
        }
    }
}
