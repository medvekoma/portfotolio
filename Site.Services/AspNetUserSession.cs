using System.Security.Principal;
using System.Web;
using Microsoft.AspNetCore.Http;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Services
{
    public class AspNetUserSession : IUserSession
    {
        private readonly HttpContext _httpContext;
        public AspNetUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public void SetValue(string key, string value)
        {
            _httpContext.Session?.SetString(key, value);
        }

        public string GetValue(string key) 
        {
            var session = _httpContext.Session;

            return session?.GetString(key);
        }
    }
}