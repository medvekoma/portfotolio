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
    }
}