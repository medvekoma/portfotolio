using FlickrNet;
using Portfotolio.Domain.Persistency;

namespace Portfotolio.FlickrEngine
{
    public interface IFlickrFactory
    {
        Flickr GetFlickr();
    }

    public class FlickrFactory : IFlickrFactory
    {
        private readonly IUserSession _userSession;

        public FlickrFactory(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public Flickr GetFlickr()
        {
            var authenticationInfo = _userSession.GetAuthenticationInfo();
            var flickr = new Flickr();
            if (authenticationInfo.IsAuthenticated)
                flickr.AuthToken = authenticationInfo.Token;

            return flickr;
        }
    }
}