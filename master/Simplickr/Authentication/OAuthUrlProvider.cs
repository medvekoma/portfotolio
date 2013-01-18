using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Simplickr.Authentication
{
    public class OAuthUrlProvider
    {
        private readonly ParameterMap _parameterMap = new ParameterMap();
        private string _normalizedUrl;
        private string _httpMethod = "GET";
        private string _consumerSecret;
        private string _tokenSecret;
        private string _normalizedParameters;

        public OAuthUrlProvider()
        {
            // set default values
            Nonce().Timestamp().SignatureMethod();
        }

        public OAuthUrlProvider HttpMethod(string httpMethod)
        {
            if (httpMethod == null)
                throw new ArgumentNullException("httpMethod");

            _httpMethod = httpMethod.ToUpper();
            return this;
        }

        public OAuthUrlProvider Nonce()
        {
            _parameterMap.Set("oauth_nonce", GenerateNonce());
            return this;
        }

        public OAuthUrlProvider Timestamp()
        {
            _parameterMap.Set("oauth_timestamp", GenerateTimestamp());
            return this;
        }

        public OAuthUrlProvider ConsumerKey(string consumerKey)
        {
            _parameterMap.Set("oauth_consumer_key", consumerKey);
            return this;
        }

        public OAuthUrlProvider ConsumerSecret(string consumerSecret)
        {
            _consumerSecret = consumerSecret;
            return this;
        }

        public OAuthUrlProvider Token(string token)
        {
            _parameterMap.Set("oauth_token", token);
            return this;
        }

        public OAuthUrlProvider TokenSecret(string tokenSecret)
        {
            _tokenSecret = tokenSecret;
            return this;
        }

        public OAuthUrlProvider Version(string version = "1.0")
        {
            _parameterMap.Set("oauth_version", version);
            return this;
        }

        public OAuthUrlProvider SignatureMethod(string signatureMethod = "HMAC-SHA1")
        {
            _parameterMap.Set("oauth_signature_method", signatureMethod);
            return this;
        }

        public OAuthUrlProvider Callback(string callback)
        {
            _parameterMap.Set("oauth_callback", EncodingUtility.UrlEncode(callback));
            return this;
        }

        public OAuthUrlProvider Verifier(string verifier)
        {
            _parameterMap.Set("oauth_verifier", verifier);
            return this;
        }

        public OAuthUrlProvider Uri(Uri uri)
        {
            _normalizedUrl = string.Format("{0}://{1}", uri.Scheme, uri.Host);
            if (!((uri.Scheme == "http" && uri.Port == 80) || (uri.Scheme == "https" && uri.Port == 443)))
            {
                _normalizedUrl += ":" + uri.Port;
            }
            _normalizedUrl += uri.AbsolutePath;

            SetQueryParameters(uri);
            return this;
        }

        public OAuthUrlProvider Url(string url)
        {
            return Uri(new Uri(url));
        }

        public string GetSignedUrl()
        {
            var signature = GetSignature();

            string url = _normalizedUrl
                + "?" + _normalizedParameters
                + "&oauth_signature=" + EncodingUtility.UrlEncode(signature);

            return url;
        }

        private string GetSignatureBase()
        {
            var elements = _parameterMap
                .Select(parameter => parameter.Key + '=' + parameter.Value)
                .ToArray();
            _normalizedParameters = string.Join("&", elements);

            var signatureBase = _httpMethod
                                + '&' + EncodingUtility.UrlEncode(_normalizedUrl)
                                + '&' + EncodingUtility.UrlEncode(_normalizedParameters);

            return signatureBase;
        }

        private string GetSignature()
        {
            if (_parameterMap.Get("oauth_signature_method") != "HMAC-SHA1")
                throw new ArgumentException("SignatureMethod HMAC-SHA1 is mandatory");

            var tokenSecret = string.IsNullOrEmpty(_tokenSecret) ? "" : EncodingUtility.UrlEncode(_tokenSecret);
            var secret = EncodingUtility.UrlEncode(_consumerSecret) + '&' + tokenSecret;
            var hmacsha1 = new HMACSHA1 {Key = Encoding.ASCII.GetBytes(secret)};

            var signatureBase = GetSignatureBase();
            return ComputeHash(hmacsha1, signatureBase);
        }

        private string ComputeHash(HashAlgorithm hashAlgorithm, string data)
        {
            if (hashAlgorithm == null)
            {
                throw new ArgumentNullException("hashAlgorithm");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            byte[] dataBuffer = Encoding.ASCII.GetBytes(data);
            byte[] hashBytes = hashAlgorithm.ComputeHash(dataBuffer);

            return Convert.ToBase64String(hashBytes);
        }

        private void SetQueryParameters(Uri uri)
        {
            const string oAuthParameterPrefix = "oauth_";
            var queryString = uri.Query;

            if (!string.IsNullOrEmpty(queryString))
            {
                string[] queryElements = queryString.Split('&');
                foreach (string queryElement in queryElements)
                {
                    if (!string.IsNullOrEmpty(queryElement) && !queryElement.StartsWith(oAuthParameterPrefix))
                    {
                        if (queryElement.IndexOf('=') > -1)
                        {
                            string[] keyValue = queryElement.Split('=');
                            _parameterMap.Set(keyValue[0], keyValue[1]);
                        }
                        else
                        {
                            _parameterMap.Set(queryElement, string.Empty);
                        }
                    }
                }
            }
        }

        #region helpers 

        private static string GenerateNonce()
        {
            var random = new Random();
            return random.Next(123400, 9999999).ToString(CultureInfo.InvariantCulture);
        }

        private static string GenerateTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }

        #endregion
    }
}