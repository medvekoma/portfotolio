using System;

namespace Portfotolio.Domain.Exceptions
{
    public class IncorrectUrlException : PortfotolioException
    {
        public override bool IsWarning { get { return true; } }

        public string Url { get; private set; }

        public IncorrectUrlException(string url)
        {
            Url = url;
        }

        public IncorrectUrlException(string url, Exception innerException) : base(innerException)
        {
            Url = url;
        }

        public override string Message
        {
            get { return string.Empty; }
        }
    }
}