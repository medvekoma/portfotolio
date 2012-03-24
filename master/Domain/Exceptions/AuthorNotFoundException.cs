using System;
using System.Runtime.Serialization;

namespace Portfotolio.Domain.Exceptions
{
    public class AuthorNotFoundException : PortfotolioException
    {
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

        public override int HttpStatusCode
        {
            get { return 404; }
        }

        public override bool IsWarning
        {
            get
            {
                return true;
            }
        }
    }
}