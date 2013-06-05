using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Helpers
{
    [Obsolete("Temporarily disabled", true)]
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
            if (!filterContext.HttpContext.Request.IsAjaxRequest() && filterContext.Result is ViewResult)
            {
                var placeholderDictionary = _placeholderDictionary ?? new Dictionary<string, object>();
                foreach (var element in filterContext.Controller.ViewData)
                {
                    placeholderDictionary[element.Key] = element.Value;
                }

                string breadCrumb = 
                    StringFormat.TokenStringFormat.Format(_pattern, placeholderDictionary);
                    // placeholderDictionary.Aggregate(_pattern, (current, element) => current.Replace('{' + element.Key + '}', ToSafeString(element.Value)));
                
                filterContext.Controller.ViewData[DataKeys.BreadCrumb] = breadCrumb;
            }
        }

        private static string ToSafeString(object value)
        {
            if (value == null)
                return string.Empty;

            return value.ToString();
        }
    }
}