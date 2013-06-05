using System.Web.Mvc;

namespace Portfotolio.Site.Helpers
{
    public class ViewResultWithStatusCode : ViewResult
    {
        private readonly int _statusCode;
        private readonly string _description;

        public ViewResultWithStatusCode(int statusCode, string description = null)
            : this(null, statusCode, description)
        {

        }

        public ViewResultWithStatusCode(string viewName, int statusCode, string description = null)
        {
            _statusCode = statusCode;
            _description = description;
            ViewName = viewName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.HttpContext;
            var response = httpContext.Response;

            response.StatusCode = _statusCode;
            response.StatusDescription = _description;

            base.ExecuteResult(context);
        }
    }
}