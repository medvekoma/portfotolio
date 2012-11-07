using System;

namespace Portfotolio.Domain.Exceptions
{
    public class AlbumNotFoundException : PortfotolioException
    {
        public override int HttpStatusCode { get { return 404; } }
        public override bool IsWarning { get { return true; } }

        public string AlbumId { get; private set; }
        
        public AlbumNotFoundException(string albumId)
        {
            AlbumId = albumId;
        }

        public AlbumNotFoundException(string albumId, Exception innerException) : base(innerException)
        {
            AlbumId = albumId;
        }

        public override string Message
        {
            get
            {
                return string.Format("Album '{0}' is not found... Maybe its owner deleted it recently.", AlbumId);
            }
        }
    }
}