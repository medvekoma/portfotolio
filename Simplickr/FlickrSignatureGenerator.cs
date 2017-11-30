using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Simplickr.Configuration;
using Simplickr.Parameters;

namespace Simplickr
{
    public interface IFlickrSignatureGenerator
    {
        void AddSignature(ParameterMap parameterMap);
    }

    public class FlickrSignatureGenerator : IFlickrSignatureGenerator
    {
        private readonly ISimplickrConfigurationProvider _simplickrConfigurationProvider;

        public FlickrSignatureGenerator(ISimplickrConfigurationProvider simplickrConfigurationProvider)
        {
            _simplickrConfigurationProvider = simplickrConfigurationProvider;
        }

        public void AddSignature(ParameterMap parameterMap)
        {
            var signatureBaseElements = parameterMap
                             .Select(parameter => parameter.Key + parameter.Value)
                             .ToArray();

            var simplickrConfig = _simplickrConfigurationProvider.GetConfig();
            var secret = simplickrConfig.Secret;
            var signatureBase = secret + string.Join("", signatureBaseElements);

            var signature = MD5Hash(signatureBase);

            parameterMap.Set("api_sig", EncodingUtility.UrlEncode(signature));
        }

        private static string MD5Hash(string data)
        {
            byte[] hashedBytes;
            using (var csp = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                hashedBytes = csp.ComputeHash(bytes, 0, bytes.Length);
            }
            return BitConverter.ToString(hashedBytes).Replace("-", String.Empty).ToLower(System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}