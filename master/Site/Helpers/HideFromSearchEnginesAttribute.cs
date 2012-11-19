using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Models;

namespace Portfotolio.Site.Helpers
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class HideFromSearchEnginesAttribute : ActionFilterAttribute
    {
        private readonly AllowRobots _allowRobots;
        private readonly Predicate<HttpRequestBase> _condition;

        private static readonly IDictionary<HideFromSearchEngineCondition, Predicate<HttpRequestBase>>
            ConditionDictionary = new Dictionary<HideFromSearchEngineCondition, Predicate<HttpRequestBase>>
                {
                    {HideFromSearchEngineCondition.Always, request => true},
                    {HideFromSearchEngineCondition.HasPageAttribute, request => request.QueryString["page"] != null}
                };

        public HideFromSearchEnginesAttribute(AllowRobots allowRobots, HideFromSearchEngineCondition condition = HideFromSearchEngineCondition.Always)
        {
            _allowRobots = allowRobots;
            _condition = ConditionDictionary[condition];
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewData = filterContext.Controller.ViewData;

            if (_condition(filterContext.HttpContext.Request))
            {
                var allowRobots = _allowRobots;
                if (viewData[DataKeys.AllowRobots] != null)
                {
                    var previousAllowRobots = (AllowRobots)viewData[DataKeys.AllowRobots];
                    allowRobots = previousAllowRobots & _allowRobots;
                }
                viewData[DataKeys.AllowRobots] = allowRobots;
            }

            base.OnResultExecuting(filterContext);
        }
    }
}