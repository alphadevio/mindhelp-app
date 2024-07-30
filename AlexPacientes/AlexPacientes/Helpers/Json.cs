using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Helpers
{
    public class Json
    {
        public static T DeserializeObject<T>(string value, bool UseCustomSettings = true)
        {
            if (UseCustomSettings)
                return JsonConvert.DeserializeObject<T>(value, GetJsonSerializerSettings());
            else
                return JsonConvert.DeserializeObject<T>(value);
        }

        public static string SerializeObject(object value, bool UseCustomSettings = true)
        {
            if(UseCustomSettings)
                return JsonConvert.SerializeObject(value, GetJsonSerializerSettings());
            else
                return JsonConvert.SerializeObject(value);
        }

        protected static JsonSerializerSettings GetJsonSerializerSettings()
        {
            var contract = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            return new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contract
            };
        }
    }
}
