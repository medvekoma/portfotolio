using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.FlickrEngine;

namespace FlickrEngineTests
{
    [TestFixture]
    public class FlickrPhotoEngineTests
    {
        [Test]
        public void GetPhotosOf_ExistingUser()
        {
            // Given
            const string userId = "existingUser";
            var flickrPhotoProvider = Substitute.For<IFlickrPhotoProvider>();
            var flickrConverter = Substitute.For<IFlickrConverter>();
            var configurationProvider = Substitute.For<IConfigurationProvider>();
            var userSession = Substitute.For<IUserSession>();
            var photoCollection = new PhotoCollection();
            flickrPhotoProvider
                .GetPhotosOf(userId, 1, Arg.Any<int>())
                .Returns(photoCollection);
            var unfilteredDmainPhotos = new DomainPhotos(new List<DomainPhoto>(), 1, 1);
            flickrConverter
                .Convert(photoCollection)
                .Returns(unfilteredDmainPhotos);
            var flickrPhotoEngine = new FlickrPhotoEngine(flickrPhotoProvider, flickrConverter, configurationProvider, userSession);

            // When
            DomainPhotos domainPhotos = flickrPhotoEngine.GetPhotosOf(userId, 1);

            // Then
            domainPhotos.Should().NotBeNull();
            flickrPhotoProvider.Received().GetPhotosOf(userId, 1, Arg.Any<int>());
        }
    }
}
