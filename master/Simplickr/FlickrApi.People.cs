using Simplickr.Parameters;
using Simplickr.Response;

namespace Simplickr
{
    public partial class FlickrApi
    {
        public FlickrPhotosResponse PeopleGetPublicPhotos(GetPhotosParameters parameters, bool sign = false)
        {
            return _flickrApiInvoker.Invoke<FlickrPhotosResponse>("flickr.people.getPublicPhotos", parameters, sign);
        }

        public FlickrPhotosResponse PeopleGetPhotos(GetPhotosParameters parameters)
        {
            return _flickrApiInvoker.Invoke<FlickrPhotosResponse>("flickr.people.getPhotos", parameters);
        }
    }
}