using System;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    public static class UserSessionHelper
    {
         public static TResult GetFromUserSession<TResult>(this HtmlHelper html, Func<IUserSession, TResult> method)
         {
             var userSession = DependencyResolver.Current.GetService<IUserSession>();
             return method(userSession);
         }
    }
}