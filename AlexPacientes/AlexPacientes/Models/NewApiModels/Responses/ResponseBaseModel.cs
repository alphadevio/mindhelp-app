using AlexPacientes.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class ResponseBase
    {
        public int Status { get; set; }
        [Newtonsoft.Json.JsonProperty("statusText")]
        public string StatusText { get; set; }
        public string Method { get; set; }
        [Newtonsoft.Json.JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }
        public Error Error { get; set; }
    }
    public class ResponseBaseModel<T>:ResponseBase
    {
        public T Data { get; set; }
    }

    public class Error
    {
        public List<ErrorDetail> Errors { get; set; }
    }

    public class ErrorDetail
    {
        public string Message { get; set; }
    }
}
