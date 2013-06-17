using System;

namespace Portfotolio.Utility.Extensions
{
    public static class BasicExtensions
    {
        public static string IfNullOrEmpty(this string testValue, string alternativeValue)
        {
            return string.IsNullOrEmpty(testValue)
                       ? alternativeValue
                       : testValue;
        }

        public static TTarget ConvertTo<TTarget>(this object source, TTarget defaultValue = default(TTarget))
        {
            try
            {
                return (TTarget)source;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}