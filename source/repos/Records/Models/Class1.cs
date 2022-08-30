using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class class1 {
        public List<string> ListGenders()
        {
            // keeping this simple
            return new List<string>() { "Female", "Male" };
        }

        

        public List<string> ListColors()
        {
            return new List<string>() { "Blue", "Green", "Red", "Yellow" };
        }
    }

}