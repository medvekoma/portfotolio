using System;
using System.Security.Principal;
using System.Web;

namespace Portfotolio.Site.Services
{
	public interface IHttpContextProvider
	{
		[Obsolete]
		HttpContextBase GetHttpContext();
		IPrincipal GetUser();
		void AddCookie(HttpCookie httpCookie);
	}

	public class HttpContextProvider : IHttpContextProvider
	{
		public HttpContextBase GetHttpContext()
		{
			var wrapper = new HttpContextWrapper(HttpContext.Current);
			return wrapper;
		}

		public IPrincipal GetUser()
		{
			return HttpContext.Current.User;
		}

		public void AddCookie(HttpCookie httpCookie)
		{
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}
	}
}