
using System.Linq;
using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site4.Attributes
{
    public class DisplayLicensingInfoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var domainPhotos = filterContext.Controller.ViewData.Model as DomainPhotos;
            if (domainPhotos != null)
            {
                var hasUnlicensedPhotos = domainPhotos.Photos.Any(photo => !photo.IsLicensed);
                if (hasUnlicensedPhotos)
                    filterContext.Controller.ViewData[DataKeys.DisplayLicensingInfoKey] = "1";
            }
        }
    }
}