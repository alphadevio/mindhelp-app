using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Services
{
    public interface IPushNotification
    {
        void Push(string title, string message);
    }
}
