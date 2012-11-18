using System;

namespace Portfotolio.Domain.Exceptions
{
    public class FlickrServiceUnavailableException : PortfotolioException
    {
        public override bool IsWarning { get { return true; } }
        public FlickrServiceUnavailableException(Exception innerException)
            : base(innerException)
        {
        }

        public override string Message
        {
            get
            {
                return "Flickr service is currently unavailable. Please try again later.";
            }
        }
    }
}