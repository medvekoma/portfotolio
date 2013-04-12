using System.Web;
using Portfotolio.Domain.Configuration;

namespace Portfotolio.Site.Services
{
    public class UserStorePathProvider : IUserStorePathProvider
    {
        public string GetOptoutFileName()
        {
            return HttpContext.Current.Server.MapPath(@"~/optout.txt");
        }

        public string GetOptinFileName()
        {
            return HttpContext.Current.Server.MapPath(@"~/optin.txt");
        }
    }
}