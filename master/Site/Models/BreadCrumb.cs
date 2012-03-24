namespace Portfotolio.Site.Models
{
    public class BreadCrumb
    {
        public string UserName { get; private set; }
        public string ActionDisplayName { get; private set; }

        public BreadCrumb(string userName, string actionDisplayName)
        {
            UserName = userName;
            ActionDisplayName = actionDisplayName;
        }
    }
}