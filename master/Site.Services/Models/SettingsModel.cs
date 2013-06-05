using Portfotolio.Domain.Configuration;

namespace Portfotolio.Site.Services.Models
{
    public class SettingsModel
    {
        public bool IsInitialized { get; set; }
        public string UserAlias { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public UserState UserState { get; set; }
    }
}