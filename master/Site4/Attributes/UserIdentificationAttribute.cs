using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Configuration;
using Portfotolio.Domain.Exceptions;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Attributes
{
    public class UserIdentificationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var userIdentifier = (string) filterContext.ActionParameters["id"];

            if(string.IsNullOrEmpty(userIdentifier))
                throw new IncorrectUrlException();

            var userEngine = DependencyResolver.Current.GetService<IUserEngine>();
            var user = userEngine.GetUser(userIdentifier);

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if (!user.IsAcceptedUserName)
                {
                    throw new OptedOutUserException(userIdentifier);
                }

                var optoutUserService = DependencyResolver.Current.GetService<IOptoutUserService>();

                var optedOutUserIds = optoutUserService.GetOptedOutUserIds();
                if (optedOutUserIds.Contains(user.UserId))
                    throw new OptedOutUserException(userIdentifier);

                if (userIdentifier != user.UserAlias)
                {
                    filterContext.RouteData.Values["id"] = user.UserAlias;
                    filterContext.Result = new RedirectToRouteResult(null, filterContext.RouteData.Values, true);
                    return;
                }

                filterContext.Controller.ViewData[DataKeys.UserIdentifier] = userIdentifier;
            }

            filterContext.Controller.ViewData[DataKeys.UserId] = user.UserId;
            filterContext.Controller.ViewData[DataKeys.UserName] = user.UserName;
        }
    }
}