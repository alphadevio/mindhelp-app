using AlexPacientes.Helpers;
using AlexPacientes.Models.NewApiModels.Responses;
using AlexPacientes.Services;
using AlexPacientes.Settings;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlexPacientes.Utilities
{
    public class RestClient
    {
        public static class FormType
        {
            public static string FormData = "multipart/form-data";
            public static string JsonApplication = "application/json";
        }

        private readonly Uri Uri = new Uri(ApiSettings.API_URL);
        private readonly Regex _regex = new Regex(@"^{\s*""message"":\s*.+\s*}");

        public async Task<T> GetAsync<T>(string method, string encKey = ApiSettings.PUBLIC_KEY)
        {
            try
            {
                HttpClient client;
                if (GlobalConfig.Identity != null)
                    client = HTTPClientFormated.GetBearerClient();
                else
                {
                    if (!ApiSettings.IS_API_PRODUCTION && Device.RuntimePlatform == Device.Android)
                        client = new HttpClient(Xamarin.Forms.DependencyService.Get<IMyOwnNetService>().GetHttpClientHandler());
                    else
                        client = new HttpClient();
                }
                client.BaseAddress = Uri;
                var response = await client.GetAsync(method);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result.Replace("\n", "");
                    var crypto = new Crypto(encKey);
                    var final = ParseResponse<T>(result, crypto);
                    (final as ResponseBase).Status = 200;
                    return final;
                }
                else
                    return ErrorConvention<T>(response, encKey);
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.ToString());
            }
            return default(T);
        }

        public async Task<T> PostAsync<T>(string method, object o, string encKey = ApiSettings.PUBLIC_KEY)
        {
            try
            {
                HttpClient client;
                if (GlobalConfig.Identity != null)
                    client = HTTPClientFormated.GetBearerClient();
                else                                    
                {
                    if (!ApiSettings.IS_API_PRODUCTION && Device.RuntimePlatform == Device.Android)
                        client = new HttpClient(Xamarin.Forms.DependencyService.Get<IMyOwnNetService>().GetHttpClientHandler());
                    else
                        client = new HttpClient();
                }
                var crypto = new Crypto(encKey);
                var postBody = new Models.NewApiModels.BaseRequestModel()
                {
                    Message = crypto.Encrypt(Json.SerializeObject(o,false))
                };
                var post = Helpers.Json.SerializeObject(postBody);
                HttpContent contenidoPOST = new StringContent(post, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Uri + method, contenidoPOST);
                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var result = (await response.Content.ReadAsStringAsync()).Replace("\n", "");
                    var final = ParseResponse<T>(result, crypto);
                    (final as ResponseBase).Status = 200;
                    return final;
                }
                else
                    return ErrorConvention<T>(response, encKey);
            }
            catch (Exception exc)
            {
                string error = exc.ToString();
            }
            return default(T);
        }

        public async Task<T> PutAsync<T>(string method, object o, string encKey = ApiSettings.PUBLIC_KEY)
        {
            try
            {
                HttpClient client;
                if (GlobalConfig.Identity != null)
                    client = HTTPClientFormated.GetBearerClient();
                else
                {
                    if (!ApiSettings.IS_API_PRODUCTION && Device.RuntimePlatform == Device.Android)
                        client = new HttpClient(Xamarin.Forms.DependencyService.Get<IMyOwnNetService>().GetHttpClientHandler());
                    else
                        client = new HttpClient();
                }
                var crypto = new Crypto(encKey);
                var postBody = new Models.NewApiModels.BaseRequestModel()
                {
                    Message = crypto.Encrypt(Json.SerializeObject(o, false))
                };
                var post = JsonConvert.SerializeObject(postBody);
                HttpContent contenidoPUT = new StringContent(post, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(Uri + method, contenidoPUT);
                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var result = response.Content.ReadAsStringAsync().Result.Replace("\n", "");
                    var final = ParseResponse<T>(result, crypto);
                    (final as ResponseBase).Status = 200;
                    return final;
                }
                else
                    return ErrorConvention<T>(response, encKey);
            }
            catch (Exception exc)
            {
                string error = exc.ToString();
            }
            return default(T);
        }

        public async Task<T> PatchAsync<T>(string method, object o, string encKey = ApiSettings.PUBLIC_KEY)
        {
            try
            {
                HttpClient client;
                if (GlobalConfig.Identity != null)
                    client = HTTPClientFormated.GetBearerClient();
                else
                    client = new HttpClient();
                var crypto = new Crypto(encKey);
                var postBody = new Models.NewApiModels.BaseRequestModel()
                {
                    Message = crypto.Encrypt(Json.SerializeObject(o, false))
                };
                var patchContent = JsonConvert.SerializeObject(postBody);
                HttpRequestMessage RequestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Uri + method);
                RequestMessage.Content = new StringContent(patchContent, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(RequestMessage);
                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var result = response.Content.ReadAsStringAsync().Result.Replace("\n", "");
                    var final = ParseResponse<T>(result, crypto);
                    (final as ResponseBase).Status = 200;
                    return final;
                }
                else
                    return ErrorConvention<T>(response, encKey);
            }
            catch (Exception exc)
            {
                string error = exc.ToString();
            }
            return default(T);
        }

        public async Task<T> DeleteAsync<T>(string method, string encKey = ApiSettings.PUBLIC_KEY)
        {
            try
            {
                HttpClient client;
                if (GlobalConfig.Identity != null)
                    client = HTTPClientFormated.GetBearerClient();
                else
                    client = new HttpClient();
                client.BaseAddress = Uri;
                var response = await client.DeleteAsync(method);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result.Replace("\n", "");
                    var crypto = new Crypto(encKey);
                    var final = ParseResponse<T>(result, crypto);
                    (final as ResponseBase).Status = 200;
                    return final;
                }
                else
                    return ErrorConvention<T>(response, encKey);
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.ToString());
            }
            return default(T);
        }

        protected T ErrorConvention<T>(HttpResponseMessage response, string encKey = ApiSettings.PUBLIC_KEY)
        {
            try
            {
                var errorResult = response.Content.ReadAsStringAsync().Result.Replace("\n", "");
                var decrypted = Helpers.Json.DeserializeObject<Models.NewApiModels.BaseRequestModel>(errorResult);

                var crypto = new Crypto(encKey);
                var dec = crypto.Decrypt(decrypted.Message);
                var result = JsonConvert.DeserializeObject<T>(dec);
                return result;
            }
            catch(Exception exc)//unhandled error, probably means data inconsinstency
            {
                var tResult = JsonConvert.DeserializeObject<T>("{ }");
                return tResult;
            }
        }

        protected T ParseResponse<T>(string json, Crypto crypto)
        {
            // Check if json is encrypted
            if (_regex.IsMatch(json))
            {
                var dataEncrypted = Json.DeserializeObject<Models.NewApiModels.BaseRequestModel>(json);
                var dataDecrypted = crypto.Decrypt(dataEncrypted.Message);
                return Json.DeserializeObject<T>(dataDecrypted);
            }
            else
            {
                return Json.DeserializeObject<T>(json);
            }
        }
    }
}
