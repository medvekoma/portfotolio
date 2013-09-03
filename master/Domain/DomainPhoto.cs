namespace Portfotolio.Domain
{
    public class DomainPhoto
    {
        public string PhotoId { get; private set; }
        public string AuthorId { get; private set; }
        public string AuthorName { get; private set; }
        public string AuthorAlias { get; private set; }
        public string Title { get; private set; }
        public string PageUrl { get; private set; }
        public string Medium640Url { get; private set; }
        public int Medium640Width { get; private set; }
        public int Medium640Height { get; private set; }
        public bool IsLicensed { get; private set; }

        public DomainPhoto()
        {
            
        }

        public DomainPhoto(string id, string authorId, string authorName, string authorAlias, string title, string pageUrl, string mediumUrl, int medium640Width, int medium640Height, bool isLicensed)
        {
            PhotoId = id;
            AuthorId = authorId;
            AuthorName = authorName;
            AuthorAlias = authorAlias;
            Title = title;
            PageUrl = pageUrl;
            Medium640Url = mediumUrl;
            Medium640Width = medium640Width;
            Medium640Height = medium640Height;
            IsLicensed = isLicensed;
        }
    }
}
