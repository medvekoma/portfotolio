using System;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
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