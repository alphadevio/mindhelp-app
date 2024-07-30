using AlexPacientes.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class SignInResponseModel : ResponseBaseModel<SignInListModel>
    {

    }

    public class SignInListModel
    {
        public List<Identity> Items { get; set; }
    }
}
