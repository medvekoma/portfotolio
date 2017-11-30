namespace Simplickr.Authentication
{
    public class OAuthRequestToken
    {
        public string OAuthProblem { get; set; }
        public bool OAuthCallbackConfirmed { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthTokenSecret { get; set; }
    }
}