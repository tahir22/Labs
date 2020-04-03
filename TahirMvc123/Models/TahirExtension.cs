using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TahirMvc123.Models
{
    public static class TahirExtension
    {
   
        public static int stringlenght(this string str)
        {  
               return str.Length;
        }

        public static bool PhoneNoValidation(this string telNo)
        {
            bool PhoneNo = false;
           
            if (System.Text.RegularExpressions.Regex.Match(telNo, "^([0-9]{10})$").Success)
            {
                PhoneNo = true;
            }
            
            return PhoneNo;
        }


        public static string TSortString(this string input)
        {
          
            string newstring = "";
            string inupstring= String.Concat(input.OrderBy(c => c));



            inupstring = inupstring.Trim(new Char[] { ',' });
            char[] characters = inupstring.ToArray();

            foreach (var vv in characters)
            {
                newstring += vv + ",";
            }

            newstring = newstring.Remove(newstring.Length - 1, 1);

            
            return new string(newstring);
        }


      
    }
}
