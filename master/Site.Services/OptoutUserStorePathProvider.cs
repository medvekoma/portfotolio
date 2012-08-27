using System.Web;
using Portfotolio.Domain.Configuration;

namespace Portfotolio.Site.Services
{
    public class OptoutUserStorePathProvider : IOptoutUserStorePathProvider
    {
        public string GetStorageFileName()
        {
            return HttpContext.Current.Server.MapPath(@"~/optout.txt");
        }
    }
}