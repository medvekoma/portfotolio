using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Helpers
{
    public class HidePagesFromSearchEnginesAttribute : ActionFilterAttribute
    {
        private readonly AllowRobots _allowRobots;
        private readonly Predicate<HttpRequestBase> _condition;

        public HidePagesFromSearchEnginesAttribute(AllowRobots allowRobots)
        {
            _allowRobots = allowRobots;
            _condition = request => request.QueryString["page"] != null;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewData = filterContext.Controller.ViewData;

            if (_condition(filterContext.HttpContext.Request))
            {
                var allowRobots = _allowRobots;
                if (viewData[DataKeys.AllowRobots] != null)
                {
                    var previousAllowRobots = (AllowRobots) viewData[DataKeys.AllowRobots];
                    allowRobots = previousAllowRobots & _allowRobots;
                }
                viewData[DataKeys.AllowRobots] = allowRobots;
            }
        }
    }
}