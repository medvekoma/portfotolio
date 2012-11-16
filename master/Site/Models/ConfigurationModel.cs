namespace Portfotolio.Site.Models
{
    public class ConfigurationModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool IsOptedOut { get; set; }
    }
}