using System;
using System.Web;

namespace Portfotolio.Site.Routing
{
    public class DelegateHttpHandler : IHttpHandler
    {

        public DelegateHttpHandler(Action<HttpContext> action, bool isReusable)
        {
            IsReusable = isReusable;
            HttpHandlerAction = action;
        }

        public bool IsReusable
        {
            get;
            private set;
        }

        public Action<HttpContext> HttpHandlerAction
        {
            get;
            private set;
        }

        public void ProcessRequest(HttpContext context)
        {
            var action = HttpHandlerAction;
            if (action != null)
            {
                action(context);
            }
        }
    }
}