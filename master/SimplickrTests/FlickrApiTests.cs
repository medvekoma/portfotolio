using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplickr;
using Simplickr.Formatters;
using Simplickr.Parameters;

namespace SimplickrTests
{
    [TestClass]
    public class FlickrApiTests
    {
        private readonly IFlickrApi _flickrApi;

        public FlickrApiTests()
        {
            ISimplickrFormatter simplickrFormatter = new SimplickrJsonFormatter();
            ISimplickrInitializer simplickrInitializer = new SimplickrInitializer();
            IHttpClient httpClient = new HttpClient();
            IFlickrRequestBuilder flickrRequestBuilder = new FlickrRequestBuilder(simplickrFormatter, simplickrInitializer);
            IFlickrApiInvoker flickrApiInvoker = new FlickrApiInvoker(flickrRequestBuilder, httpClient, simplickrFormatter);
            _flickrApi = new FlickrApi(flickrApiInvoker);
        }

        [TestMethod]
        public void PeopleGetPublicPhotosTest()
        {
            var request = new GetPhotosParameters(userId: "27725019@N00")
                .PerPage(30)
                .Extras(Extras.PathAlias | Extras.UrlS);
            var result = _flickrApi.PeopleGetPublicPhotos(request);

            Assert.AreEqual("ok", result.Stat);
            Assert.AreEqual(30, result.Photos.PerPage);
            Assert.AreEqual(1, result.Photos.Page);
        }


        [TestMethod]
        public void PeopleGetPhotosTestUnauthorized()
        {
            var request = new GetPhotosParameters(userId: "27725019@N00")
                .PerPage(30)
                .Extras(Extras.PathAlias | Extras.UrlS);
            var result = _flickrApi.PeopleGetPhotos(request);

            Assert.AreEqual("fail", result.Stat);
            Assert.AreEqual(99, result.Code);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
        }
    }
}
