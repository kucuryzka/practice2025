using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input = input.ToLower();
            char[] arr = input.ToCharArray();
            string res = new String(arr.Where(ch => !char.IsWhiteSpace(ch) && !char.IsPunctuation(ch)).ToArray());

            return res.Equals(new String(res.Reverse().ToArray()));
            
        }
    }
}
