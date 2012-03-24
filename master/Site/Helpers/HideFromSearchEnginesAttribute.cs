using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    public class HideFromSearchEnginesAttribute : ActionFilterAttribute
    {
        private readonly bool _hide;

        public HideFromSearchEnginesAttribute(bool hide = true)
        {
            _hide = hide;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult != null)
            {
                if (_hide)
                {
                    viewResult.ViewData[DataKeys.HideFromSearchEngines] = true;
                }
            }
            base.OnResultExecuting(filterContext);
        }
    }
}