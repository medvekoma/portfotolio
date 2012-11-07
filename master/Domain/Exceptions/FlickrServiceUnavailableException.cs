using System;

namespace Portfotolio.Domain.Exceptions
{
    public class FlickrServiceUnavailableException : PortfotolioException
    {
        public override int HttpStatusCode { get { return 503; } }

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