using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Helpers
{
    public class HideFromSearchEnginesAttribute : ActionFilterAttribute
    {
        private readonly AllowRobots _allowRobots;
        private readonly Predicate<RouteValueDictionary> _condition;

        public HideFromSearchEnginesAttribute(AllowRobots allowRobots)
        {
            _allowRobots = allowRobots;
            _condition = routeData => true;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var viewData = filterContext.Controller.ViewData;

            if (_condition(filterContext.RouteData.Values))
            {
                var allowRobots = _allowRobots;
                if (viewData[DataKeys.AllowRobots] != null)
                {
                    var previousAllowRobots = (AllowRobots)viewData[DataKeys.AllowRobots];
                    allowRobots = previousAllowRobots & _allowRobots;
                }
                viewData[DataKeys.AllowRobots] = allowRobots;
            }
        }
    }
}