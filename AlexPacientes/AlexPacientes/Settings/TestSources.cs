using System;
using System.Collections.Generic;
using System.Text;

namespace AlexPacientes.Settings
{
    public static class TestSources
    {
        public static string[] Names = new string[]
              {
                "Ricardo",
                "Leonardo",
                "Javier",
                "Eduardo",
                "Marinana"
              };

        public static string[] Lastnames = new string[]
        {
                "Lopez",
                "Sanchez",
                "Leyva",
                "Oliva"
        };
        public static string GetRandomName()
        {
            Random r = new Random();
            return Names[r.Next(0, Names.Length - 1)] + " " + Lastnames[r.Next(0, Lastnames.Length - 1)] + " " + Lastnames[r.Next(0, Lastnames.Length - 1)];
        }
        //Function because it could be a random message too
        public static string GetLoremMessage()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        }
    }
}
