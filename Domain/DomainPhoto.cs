namespace Portfotolio.Domain
{
    public class DomainPhoto
    {
        public string Id { get; private set; }
        public string AuthorId { get; private set; }
        public string AuthorName { get; private set; }
        public string AuthorAlias { get; private set; }
        public string Title { get; private set; }
        public string PageUrl { get; private set; }
        public string SmallUrl { get; private set; }
        public int SmallWidth { get; private set; }
        public int SmallHeight { get; private set; }
        public bool IsLicensed { get; private set; }

        public DomainPhoto(string id, string authorId, string authorName, string authorAlias, string title, string pageUrl, string smallUrl, int smallWidth, int smallHeight, bool isLicensed)
        {
            Id = id;
            AuthorId = authorId;
            AuthorName = authorName;
            AuthorAlias = authorAlias;
            Title = title;
            PageUrl = pageUrl;
            SmallUrl = smallUrl;
            SmallWidth = smallWidth;
            SmallHeight = smallHeight;
            IsLicensed = isLicensed;
        }
    }
}
