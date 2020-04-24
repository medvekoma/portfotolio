using System;
using System.Web;
using Newtonsoft.Json;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Services
{
	public class FormsAuthenticationStorage : IAuthenticationStorage
	{
		private readonly IHttpContextProvider _httpContextProvider;

		public FormsAuthenticationStorage(IHttpContextProvider httpContextProvider)
		{
			_httpContextProvider = httpContextProvider;
		}

		public AuthenticationInfo GetAuthenticationInfo()
		{
			var user = _httpContextProvider.GetUser();
			if (!user.Identity.IsAuthenticated)
				return new AuthenticationInfo();

			var formsIdentity = user.Identity as FormsIdentity;
			if (formsIdentity == null)
				return new AuthenticationInfo();

			var authenticationInfo = JsonConvert.DeserializeObject<AuthenticationInfo>(formsIdentity.Ticket.UserData);
			return authenticationInfo;
		}

		public void SetAuthenticationInfo(AuthenticationInfo authenticationInfo)
		{
			string userData = JsonConvert.SerializeObject(authenticationInfo);
			var formsAuthenticationTicket = new FormsAuthenticationTicket(1, authenticationInfo.UserAlias,
				DateTime.Now, DateTime.Now.AddHours(1),
				false, userData);
			string encryptedCookie = FormsAuthentication.Encrypt(formsAuthenticationTicket);
			var httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
			_httpContextProvider.AddCookie(httpCookie);
		}

		public void Clear()
		{
			FormsAuthentication.SignOut();
		}
	}
}