using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Exceptions;

namespace Portfotolio.Site4.Attributes
{
    public class RejectOptedOutUsersAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.ActionParameters.ContainsKey("id"))
            {
                var userIdentifier = (string)filterContext.ActionParameters["id"];

                var userEngine = DependencyResolver.Current.GetService<IUserEngine>();
                var user = userEngine.GetUser(userIdentifier);

                if (!user.IsAcceptedUserName)
                {
                    throw new OptedOutUserException(userIdentifier);
                }

                var optoutUserService = DependencyResolver.Current.GetService<IUserService>();
                var optedOutUserIds = optoutUserService.GetOptoutUserIds();
                if (optedOutUserIds.Contains(user.UserId))
                    throw new OptedOutUserException(userIdentifier);
            }
        }
    }
}