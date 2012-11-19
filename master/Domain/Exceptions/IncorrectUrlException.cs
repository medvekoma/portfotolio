using System;

namespace Portfotolio.Domain.Exceptions
{
    public class IncorrectUrlException : PortfotolioException
    {
        public override string Message
        {
            get { return "Incorrect request url"; }
        }
    }
}