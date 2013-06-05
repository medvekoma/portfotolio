using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    public class SetMasterViewDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var userSession = DependencyResolver.Current.GetService<IUserSession>();
                filterContext.Controller.ViewData[DataKeys.AuthenticationInfo] = userSession.GetAuthenticationInfo();
            }
        }
    }
}