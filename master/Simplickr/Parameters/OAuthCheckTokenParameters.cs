namespace Simplickr.Parameters
{
    public class OAuthCheckTokenParameters : IRequestParameters
    {
        public ParameterMap ParameterMap { get; private set; }

        public OAuthCheckTokenParameters(string token)
        {
            ParameterMap = new ParameterMap
                {
                    {"oauth_token", token}
                };
        }
    }
}