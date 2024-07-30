using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.AppModels
{
    public class Rating
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
