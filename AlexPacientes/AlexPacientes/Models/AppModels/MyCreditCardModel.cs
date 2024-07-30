using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AlexPacientes.Models.AppModels
{
    public class MyCreditCardModel : NewApiModels.Responses.CreditCard, INotifyPropertyChanged
    {
        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public MyCreditCardModel(NewApiModels.Responses.CreditCard model)
        {
            Id = model.Id;
            Object = model.Object;
            Type = model.Type;
            CreatedAt = model.CreatedAt;
            Last4 = model.Last4;
            Bin = model.Bin;
            ExpMonth = model.ExpMonth;
            ExpYear = model.ExpYear;
            Brand = model.Brand;
            Name = model.Name;
            ParentId = model.ParentId;
            Default = model.Default;
            CardIndex = model.CardIndex;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
