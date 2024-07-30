using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Models.AppModels
{
    public class Doctor
    {
        public string Image { get; set; }
        public string Area { get; set; }
        public string Name { get; set; }
        public double Rate { get; set; }
        public int Feedbacks { get; set; }
        public int YearsExp { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Speciality { get; set; }
    }
}
