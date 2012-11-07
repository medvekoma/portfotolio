using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    public class BreadCrumbAttribute : ActionFilterAttribute
    {
        private readonly string _pattern;
        private Dictionary<string, object> _placeholderDictionary;

        public BreadCrumbAttribute(string pattern)
        {
            _pattern = pattern;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                _placeholderDictionary = new Dictionary<string, object>(filterContext.ActionParameters);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                _placeholderDictionary = _placeholderDictionary ?? new Dictionary<string, object>();
                foreach (var element in filterContext.Controller.ViewData)
                {
                    _placeholderDictionary[element.Key] = element.Value;
                }
                var placeholderDictionary = _placeholderDictionary
                    .Where(item => item.Value != null)
                    .ToDictionary(item => item.Key, item => item.Value);
                string breadCrumb = placeholderDictionary.Aggregate(_pattern, (current, element) => current.Replace('{' + element.Key + '}', element.Value.ToString()));
                filterContext.Controller.ViewData[DataKeys.BreadCrumb] = breadCrumb;
                _placeholderDictionary.Clear();
            }
        }
    }
}