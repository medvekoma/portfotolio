using Simplickr.Parameters;
using Simplickr.Response;

namespace Simplickr
{
    public partial class FlickrApi
    {
        public FlickrPhotosResponse PeopleGetPublicPhotos(GetPhotosParameters parameters)
        {
            return _flickrApiInvoker.Invoke<FlickrPhotosResponse>("flickr.people.getPublicPhotos", parameters);
        }
    }
}