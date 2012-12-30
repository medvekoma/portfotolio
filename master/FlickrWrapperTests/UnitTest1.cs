using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FlickrWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlickrWrapperTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var flickrApi = new FlickrApi("");
            var response = flickrApi.PeopleGetPublicPhotos("27725019@N00");

            Console.WriteLine(response);
        }
    }
}
