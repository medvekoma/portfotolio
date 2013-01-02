using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simplickr.Authentication;
using Simplickr.Configuration;

namespace SimplickrTests
{
    [TestClass]
    public class OAuthTests
    {
        private readonly OAuthService _oAuthService;

        public OAuthTests()
        {
            _oAuthService = new OAuthService(new SimplickrConfigurationProvider());
        }

        [TestMethod]
        public void OAuthTest()
        {
            string requestTokenUrl = _oAuthService.GetRequestTokenUrl();

            Console.WriteLine(requestTokenUrl);
        }
    }
}
