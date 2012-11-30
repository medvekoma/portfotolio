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

        public static Flickr UnauthenticatedFlickr = new Flickr();

        public Flickr GetFlickr()
        {
            var authenticationInfo = _userSession.GetAuthenticationInfo();
            if (!authenticationInfo.IsAuthenticated)
                return UnauthenticatedFlickr;

            return new Flickr
                       {
                           AuthToken = authenticationInfo.Token,
                       };
        }
    }
}