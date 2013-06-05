using System.Web.Mvc;
using Portfotolio.Domain;

namespace Portfotolio.Site4.Attributes
{
    public class RedirectToUserAliasAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.ActionParameters.ContainsKey("id"))
            {
                var userIdentifier = (string)filterContext.ActionParameters["id"];

                var userEngine = DependencyResolver.Current.GetService<IUserEngine>();
                var user = userEngine.GetUser(userIdentifier);

                if (userIdentifier != user.UserAlias)
                {
                    filterContext.RouteData.Values["id"] = user.UserAlias;
                    filterContext.Result = new RedirectToRouteResult(null, filterContext.RouteData.Values, true);
                }
            }
        }
    }
}