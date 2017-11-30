using System;

namespace Portfotolio.Domain.Exceptions
{
    public class AuthorNotFoundException : PortfotolioException
    {
        public override bool IsWarning { get { return true; } }

        public string AuthorName { get; private set; }

        public AuthorNotFoundException(string authorName)
        {
            AuthorName = authorName;
        }

        public AuthorNotFoundException(string authorName, Exception innerException) : base(innerException)
        {
            AuthorName = authorName;
        }

        private const string MessageFormat = "Author '{0}' is not found (maybe he/she is no longer active on flickr).";

        public override string Message
        {
            get { return string.Format(MessageFormat, AuthorName); }
        }
    }
}