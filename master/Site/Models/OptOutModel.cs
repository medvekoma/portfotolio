namespace Portfotolio.Site.Models
{
    public class OptOutModel
    {
        public string UserName { get; private set; }
        public bool IsOptedOut { get; private set; }

        public OptOutModel(string userName, bool isOptedOut)
        {
            UserName = userName;
            IsOptedOut = isOptedOut;
        }
    }
}