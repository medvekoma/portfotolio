using System.Web;
using Services;

namespace Portfotolio.Site
{
    public class OptoutUserStorePathProvider : IOptoutUserStorePathProvider
    {
        public string GetStorageFileName()
        {
            return HttpContext.Current.Server.MapPath(@"~/optout.txt");
        }
    }
}