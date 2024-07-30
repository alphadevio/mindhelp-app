using AlexPacientes.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.NewApiModels.Responses
{
    public class SignUpResponseModel : ResponseBaseModel<SignUpListModel>
    {
        
    }

    public class SignUpListModel
    {
        public List<Identity> Items { get; set; }
    }
}
