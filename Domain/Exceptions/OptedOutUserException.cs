using System;

namespace Portfotolio.Domain.Exceptions
{
    public class OptedOutUserException : PortfotolioException
    {
        public override bool IsWarning { get { return true; } }

        public string UserAlias { get; private set; }

        public OptedOutUserException(string userAlias)
        {
            UserAlias = userAlias;
        }

        public OptedOutUserException(string userAlias, Exception innerException) : base(innerException)
        {
            UserAlias = userAlias;
        }

        public override string Message
        {
            get { return "Nothing to see here. Photos are hidden on their author's request."; }
        }
    }
}