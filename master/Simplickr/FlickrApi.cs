﻿using Simplickr.Parameters;
using Simplickr.Response;

namespace Simplickr
{
    public interface IFlickrApi
    {
        FlickrPhotosResponse PeopleGetPublicPhotos(GetPhotosParameters parameters, bool sign = false);
        FlickrPhotosResponse PeopleGetPhotos(GetPhotosParameters parameters);
    }

    public partial class FlickrApi : IFlickrApi
    {
        private readonly IFlickrApiInvoker _flickrApiInvoker;

        public FlickrApi(IFlickrApiInvoker flickrApiInvoker)
        {
            _flickrApiInvoker = flickrApiInvoker;
        }
    }
}