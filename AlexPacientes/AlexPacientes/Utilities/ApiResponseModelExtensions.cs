using AlexPacientes.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Utilities
{
    public static class ApiResponseModelExtensions
    {
        public static bool HasValidStatus<T>(this Models.NewApiModels.Responses.ResponseBaseModel<T> response)
        {
            return response.Status == ApiSettings.Status.SUCCESS || response.Status == ApiSettings.Status.CREATED;
        }
    }
}
