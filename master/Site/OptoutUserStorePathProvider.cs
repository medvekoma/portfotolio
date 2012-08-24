using System;
using System.Web;

namespace Portfotolio.Site
{
    public interface IOptoutUserStorePathProvider
    {
        string GetStorageFileName();
    }

    public class OptoutUserStorePathProvider : IOptoutUserStorePathProvider
    {
        public string GetStorageFileName()
        {
            return HttpContext.Current.Server.MapPath(@"~/optout.txt");
        }
    }
}