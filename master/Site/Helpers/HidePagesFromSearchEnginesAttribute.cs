using System;
using System.Globalization;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    public class HidePagesFromSearchEnginesAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult != null)
            {
                var pageString = filterContext.RequestContext.RouteData.Values["page"] as string;
                if (pageString != null)
                {
                    int page;
                    if (Int32.TryParse(pageString, NumberStyles.Integer, CultureInfo.InvariantCulture, out page) && page > 1)
                    {
                        viewResult.ViewData[DataKeys.HideFromSearchEngines] = true;
                    }
                }
            }
            base.OnResultExecuting(filterContext);
        }
    }
}