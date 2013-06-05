namespace Portfotolio.Site.Models
{
    public class HomeModel
    {
        public string SearchText { get; set; }
        public SearchSource SearchSource { get; set; }

        public HomeModel()
        {
        }

        public HomeModel(string searchText, SearchSource searchSource)
        {
            SearchText = searchText;
            SearchSource = searchSource;
        }
    }

    public enum SearchSource
    {
        People,
        Photos,
        Groups,
    }
}