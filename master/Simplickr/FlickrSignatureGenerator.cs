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
        void AddSignature(IRequestParameters requestParameters);
    }

    public class FlickrSignatureGenerator : IFlickrSignatureGenerator
    {
        private readonly ISimplickrConfigurationProvider _simplickrConfigurationProvider;

        public FlickrSignatureGenerator(ISimplickrConfigurationProvider simplickrConfigurationProvider)
        {
            _simplickrConfigurationProvider = simplickrConfigurationProvider;
        }

        public void AddSignature(IRequestParameters requestParameters)
        {
            var signatureBaseElements = requestParameters.ParameterMap
                             .Select(parameter => parameter.Key + parameter.Value)
                             .ToArray();

            var simplickrConfig = _simplickrConfigurationProvider.GetConfig();
            var secret = simplickrConfig.Secret;
            var signatureBase = secret + string.Join("", signatureBaseElements);

            var signature = MD5Hash(signatureBase);

            requestParameters.ParameterMap["api_sig"] = EncodingUtility.UrlEncode(signature);
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