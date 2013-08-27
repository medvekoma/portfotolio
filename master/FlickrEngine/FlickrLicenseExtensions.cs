using System.Linq;
using System.Web.Mvc;
using FlickrNet;
using Portfotolio.Domain.Configuration;

namespace Portfotolio.FlickrEngine
{
    public static class FlickrLicenseExtensions
    {
        private static readonly LicenseType[] CreativeCommonsLicenses = new[]
                                                                            {
                                                                                LicenseType.AttributionCC,
                                                                                LicenseType.AttributionNoDerivativesCC, 
                                                                                LicenseType.AttributionNoncommercialCC, 
                                                                                LicenseType.AttributionNoncommercialNoDerivativesCC, 
                                                                                LicenseType.AttributionNoncommercialShareAlikeCC, 
                                                                                LicenseType.AttributionShareAlikeCC, 
                                                                            };

        public static bool IsLicensed(this Photo photo)
        {
            var userId = photo.UserId;
            var optinUserIds = DependencyResolver.Current.GetService<IUserService>().GetOptinUserIds();

            return CreativeCommonsLicenses.Contains(photo.License) || optinUserIds.Contains(userId);
        }

    }
}