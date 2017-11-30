using System;
using System.Web;
using Portfotolio.Domain.Exceptions;

namespace Portfotolio.Site4.Extensions
{
    public static class HttpExtensions
    {
         public static void SetHttpHeader(this HttpResponseBase response, Exception exception)
         {
             if (exception is AlbumNotFoundException 
                 || exception is AuthorNotFoundException 
                 || exception is IncorrectUrlException)
                 response.StatusCode = 400;
         }
    }
}