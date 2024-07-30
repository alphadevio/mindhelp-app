using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.AppModels.Payments
{
    public class PaymentCardModel
    {
        public string AccountHolder { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public int Month { get; set; } = 0;
        public int Year { get; set; } = 0;

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(AccountHolder) ||
                string.IsNullOrWhiteSpace(CardNumber) ||
                string.IsNullOrWhiteSpace(CVC) ||
                Month == 0 ||
                Year == 0)
                return false;
            return true;
        }
    }
}
