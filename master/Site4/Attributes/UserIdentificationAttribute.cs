using System.Web.Mvc;
using Portfotolio.Domain;
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

            filterContext.Controller.ViewData[DataKeys.UserId] = user.UserId;
            filterContext.Controller.ViewData[DataKeys.UserName] = user.UserName;
            filterContext.Controller.ViewData[DataKeys.UserAlias] = user.UserAlias;
        }
    }
}