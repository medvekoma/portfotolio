using System;
using System.Text;

namespace Simplickr
{
    public class EncodingUtility
    {
        /// <summary>
        /// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
        /// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
        /// </summary>
        public static string UrlEncode(string value)
        {
            const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            var stringBuilder = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    stringBuilder.Append(symbol);
                }
                else
                {
                    stringBuilder.Append('%' + String.Format("{0:X2}", (int)symbol));
                }
            }

            return stringBuilder.ToString();
        }
         
    }
}