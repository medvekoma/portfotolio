using System;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Attributes
{
    [Obsolete("Using UrlReferrer now", true)]
    public class RememberActionUrlAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Controller.TempData[DataKeys.ActionUrl] = filterContext.HttpContext.Request.RawUrl;
            }
        }
    }
}