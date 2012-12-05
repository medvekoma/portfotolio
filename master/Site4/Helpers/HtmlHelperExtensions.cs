using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string UserIdentifier(this HtmlHelper htmlHelper)
        {
            var session = htmlHelper.ViewContext.HttpContext.Session;
            if (session == null)
                return null;
            return session[DataKeys.UserIdentifier] as string;
        }
    }
}