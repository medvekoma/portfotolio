using System;

namespace Simplickr.Authentication
{
    public interface IOAuthResponseProcessor
    {
        OAuthResponse ProcessResponse(string response);
    }

    public class OAuthResponseProcessor : IOAuthResponseProcessor
    {
        public OAuthResponse ProcessResponse(string response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            var oAuthResponse = new OAuthResponse();

            var elements = response.Split('&');
            foreach (var element in elements)
            {
                var keyValue = element.Split('=');
                var key = keyValue[0];
                var value = keyValue[1];
                switch (key)
                {
                    case "oauth_problem":
                        oAuthResponse.Problem = value;
                        break;
                    case "oauth_callback_confirmed":
                        oAuthResponse.CallbackConfirmed = bool.Parse(value);
                        break;
                    case "oauth_token":
                        oAuthResponse.Token = value;
                        break;
                    case "oauth_token_secret":
                        oAuthResponse.TokenSecret = value;
                        break;
                }
            }
            return oAuthResponse;
        }
    }
}