namespace Portfotolio.Domain
{
    public class Album
    {
        public string AuthorId { get; private set; }
        public string PhotosetId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }

        public Album(string authorId, string photosetId, string title, string imageUrl)
        {
            AuthorId = authorId;
            PhotosetId = photosetId;
            Title = title;
            ImageUrl = imageUrl;
        }
    }
}