namespace Simplickr.Authentication
{
    public class OAuthRequestToken
    {
        public string Problem { get; set; }
        public bool CallbackConfirmed { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
    }
}