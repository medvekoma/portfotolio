using Portfotolio.Domain;

namespace Portfotolio.Site.Services
{
    public class CachedApplicationConfigurationProvider : IApplicationConfigurationProvider
    {
        private readonly IApplicationConfigurationProvider _applicationConfigurationProvider;
        private ApplicationConfiguration _applicationConfiguration;

        public CachedApplicationConfigurationProvider(IApplicationConfigurationProvider applicationConfigurationProvider)
        {
            _applicationConfigurationProvider = applicationConfigurationProvider;
        }

        public ApplicationConfiguration GetApplicationConfiguration()
        {
            if (_applicationConfiguration == null)
                _applicationConfiguration = _applicationConfigurationProvider.GetApplicationConfiguration();

            return _applicationConfiguration;
        }
    }
}