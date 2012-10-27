using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    public class HideFromSearchEnginesAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult != null)
            {
                viewResult.ViewData[DataKeys.HideFromSearchEngines] = true;
            }
            base.OnResultExecuting(filterContext);
        }
    }
}