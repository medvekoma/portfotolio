using System;

namespace Portfotolio.Domain.Exceptions
{
    public abstract class PortfotolioException : Exception
    {
        protected PortfotolioException()
        {
        }

        protected PortfotolioException(Exception innerException) : base(string.Empty, innerException)
        {
        }

        public virtual int HttpStatusCode
        {
            get { return 200; }
        }
    }
}
