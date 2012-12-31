using System.Collections.Generic;

namespace Simplickr.Response
{
    public class FlickrPhotos
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public IList<FlickrPhoto> Photo { get; set; }
    }

    public class FlickrPhoto
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Secret { get; set; }
        public string Server { get; set; }
        public string Farm { get; set; }
        public string Title { get; set; }
        public int IsPublic { get; set; }
        public int IsFriend { get; set; }
        public int IsFamily { get; set; }
        public string PathAlias { get; set; }
        public string Url_S { get; set; }
        public int Height_S { get; set; }
        public int Width_S { get; set; }
    }
}