using System.Drawing;

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
        public string MediumUrl { get; private set; }
        public string LargeUrl { get; private set; }
        public int SmallWidth { get; private set; }
        public int SmallHeight { get; private set; }

        public DomainPhoto(string id, string authorId, string authorName, string authorAlias, string title, string pageUrl, string smallUrl, string mediumUrl, string largeUrl, int smallWidth, int smallHeight)
        {
            Id = id;
            AuthorId = authorId;
            AuthorName = authorName;
            AuthorAlias = authorAlias;
            Title = title;
            PageUrl = pageUrl;
            SmallUrl = smallUrl;
            MediumUrl = mediumUrl;
            LargeUrl = largeUrl;
            SmallWidth = smallWidth;
            SmallHeight = smallHeight;
        }
    }
}
