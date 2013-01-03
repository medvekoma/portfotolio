using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplickr.Authentication;
using Simplickr.Configuration;

namespace SimplickrTests
{
    [TestClass]
    public class OAuthUrlServiceTests
    {
        private readonly OAuthUrlService _oAuthUrlService;

        public OAuthUrlServiceTests()
        {
            _oAuthUrlService = new OAuthUrlService(new SimplickrConfigurationProvider());
        }

        [TestMethod]
        public void ShouldGetRequestTokenUrl()
        {
            string requestTokenUrl = _oAuthUrlService.GetRequestTokenUrl("http://portfotolio.net/-account/authorize");

            Console.WriteLine(requestTokenUrl);
            Assert.IsNotNull(requestTokenUrl);
        }
    }
}
