using System.Text.RegularExpressions;
using Portfotolio.Domain;
using Portfotolio.Utility.Extensions;

namespace Portfotolio.FlickrEngine
{
    public class FlickrUserEngine : IUserEngine
    {
        private readonly IFlickrPhotoProvider _flickrPhotoProvider;

        private static readonly Regex UserIdRegex = new Regex(@"\d{3,12}\@N\d{2}");

        public FlickrUserEngine(IFlickrPhotoProvider flickrPhotoProvider)
        {
            _flickrPhotoProvider = flickrPhotoProvider;
        }

        public DomainUser GetUser(string userIdentifier)
        {
            var matchUserId = UserIdRegex.Match(userIdentifier);
            if (matchUserId.Success)
            {
                var person = _flickrPhotoProvider.GetUserByUserId(userIdentifier);
                var isAcceptedUserName = _flickrPhotoProvider.IsAcceptedUserName(person.UserName);
                var userAlias = person.PathAlias.IfNullOrEmpty(person.UserId);
                return new DomainUser(person.UserId, person.UserName, userAlias, isAcceptedUserName);
            }
            else
            {
                var foundUser = _flickrPhotoProvider.GetUserByPathAlias(userIdentifier);
                var isAcceptedUserName = _flickrPhotoProvider.IsAcceptedUserName(foundUser.UserName);
                var userAlias = userIdentifier;
                return new DomainUser(foundUser.UserId, foundUser.UserName, userAlias, isAcceptedUserName);
            }
        }
        
    }
}