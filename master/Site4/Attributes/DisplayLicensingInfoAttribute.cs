
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Attributes
{
    public class DisplayLicensingInfoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.Controller.ViewData[DataKeys.DisplayLicensingInfoKey] = "1";
        }
    }
}