using System;

namespace Simplickr.Response
{
    public static class ResponseExtensions
    {
         public static bool IsSuccessful(this FlickrResponseBase flickrResponseBase)
         {
             return string.Equals(flickrResponseBase.Stat, "ok", StringComparison.CurrentCultureIgnoreCase);
         }
    }
}