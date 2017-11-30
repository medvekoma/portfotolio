using System;

namespace Portfotolio.Domain.Exceptions
{
    public class NotEnoughPermissionsException : PortfotolioException
    {
        public override bool IsWarning { get { return true; } }

        public string ResourceId { get; private set; }

        public NotEnoughPermissionsException(string resourceId)
        {
            ResourceId = resourceId;
        }

        public NotEnoughPermissionsException(string resourceId, Exception innerException) : base(innerException)
        {
            ResourceId = resourceId;
        }

        public override string Message
        {
            get
            {
                const string format = "You don't have enough permissions to view this set ({0}). Logged in users have more privileges.";
                return string.Format(format, ResourceId);
            }
        }
    }
}