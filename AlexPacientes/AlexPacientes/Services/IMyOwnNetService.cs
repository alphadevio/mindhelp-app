using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AlexPacientes.Services
{
    public interface IMyOwnNetService
    {
        HttpClientHandler GetHttpClientHandler();
    }
}
