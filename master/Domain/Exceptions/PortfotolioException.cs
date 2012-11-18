using System;

namespace Portfotolio.Domain.Exceptions
{
    public abstract class PortfotolioException : Exception
    {
        public virtual bool IsWarning { get { return false; } }

        protected PortfotolioException() { }

        protected PortfotolioException(Exception innerException) : base(string.Empty, innerException) { }

    }
}
