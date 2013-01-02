using System.Configuration;

namespace Simplickr.Configuration
{
    public interface ISimplickrConfigurationProvider
    {
        SimplickrConfig GetConfig();
    }

    public class SimplickrConfigurationProvider : ISimplickrConfigurationProvider
    {
         public SimplickrConfig GetConfig()
         {
             var simplickrConfig = (SimplickrConfig) ConfigurationManager.GetSection("simplickr");
             return simplickrConfig;
         }
    }
}