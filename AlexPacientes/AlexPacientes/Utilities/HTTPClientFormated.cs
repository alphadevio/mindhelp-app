using AlexPacientes.Services;
using AlexPacientes.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

/// <summary>
/// Formatted clients, one with bearer token (if exist) and other with an empty headers
/// </summary>
namespace AlexPacientes.Utilities
{
    public class HTTPClientFormated
    {
        public static HttpClient GetBearerClient()
        {
            var cliente = GetDefaultHttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GlobalConfig.Token);
            return cliente;
        }
        private static HttpClient GetDefaultHttpClient()
        {
            HttpClient client;
            if (!ApiSettings.IS_API_PRODUCTION && Device.RuntimePlatform == Device.Android)
                client = new HttpClient(Xamarin.Forms.DependencyService.Get<IMyOwnNetService>().GetHttpClientHandler());
            else
                client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(60);
            return client;
        }
    }
}
