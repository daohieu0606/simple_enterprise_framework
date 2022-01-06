using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLibrary
{
    public class StringHelper
    {
        private static int length = 7;

        public static int Length { get => length; set => length = value; }

        public static string GenerateRandomString()
        {
            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < Length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }
            return str_build.ToString();
        }
    }
}
