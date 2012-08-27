using System.Web;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.Site.Services
{
    public class AspNetUserSession : IUserSession
    {
        public void SetValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public T GetValue<T>(string key) 
        {
            var session = HttpContext.Current.Session;
            if (session == null)
                return default(T);

            object value = session[key];
            if (value == null)
                return default(T);

            return (T) value;
        }
    }
}