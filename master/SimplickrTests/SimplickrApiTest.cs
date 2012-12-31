using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplickr;

namespace SimplickrTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SimplickrApiTest
    {
        private readonly ISimplickrApi _simplickrApi;

        public SimplickrApiTest()
        {
            _simplickrApi = new SimplickrApi(new SimplickrInitializer(), new SimplickrFormatter(), new SimplickrInvoker());
        }

        [TestMethod]
        public void GetPublicPhotosTest()
        {
            var request = new GetPublicPhotosRequest(userId: "27725019@N00")
                .PerPage(30)
                .Extras(Extras.PathAlias | Extras.UrlS);
            var result = _simplickrApi.GetPublicPhotos(request);

            Assert.AreEqual("ok", result.Stat);
            Assert.AreEqual(30, result.Photos.PerPage);
            Assert.AreEqual(1, result.Photos.Page);
        }
    }
}
