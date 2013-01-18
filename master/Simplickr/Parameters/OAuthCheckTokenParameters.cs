namespace Simplickr.Parameters
{
    public class OAuthCheckTokenParameters : RequestParametersBase<OAuthCheckTokenParameters>
    {
        public OAuthCheckTokenParameters(string token)
        {
            ParameterMap.Set("oauth_token", token);
        }
    }
}