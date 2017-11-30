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
	    private readonly IAuthenticationStorage _authenticationStorage;

        public FlickrFactory(IAuthenticationStorage authenticationStorage)
        {
	        _authenticationStorage = authenticationStorage;
        }

	    public Flickr GetFlickr()
	    {
		    var authenticationInfo = _authenticationStorage.GetAuthenticationInfo();
            var flickr = new Flickr();
            if (authenticationInfo.IsAuthenticated)
                flickr.AuthToken = authenticationInfo.Token;

            return flickr;
        }
    }
}