using Portfotolio.Domain.Configuration;

namespace Portfotolio.Site.Services.Models
{
    public class ConfigurationModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserAlias { get; set; }
        public string UserId { get; set; }
        public UserState UserState { get; set; }
    }
}