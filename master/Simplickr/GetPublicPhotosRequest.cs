using System.Globalization;

namespace Simplickr
{
    public class GetPublicPhotosRequest : ISimplickrRequest
    {
        public ParameterMap ParameterMap { get; private set; }

        public GetPublicPhotosRequest(string userId)
        {
            ParameterMap = new ParameterMap();
            ParameterMap.Add("user_id", userId);
        }

        public GetPublicPhotosRequest SafeSearch(SafeSearch safeSearch)
        {
            ParameterMap.Add("safe_search", safeSearch);
            return this;
        }

        public GetPublicPhotosRequest Extras(Extras extras)
        {
            ParameterMap.Add("extras", extras);
            return this;            
        }

        public GetPublicPhotosRequest PerPage(int perPage)
        {
            ParameterMap.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
            return this;            
        }

        public GetPublicPhotosRequest Page(int page)
        {
            ParameterMap.Add("page", page.ToString(CultureInfo.InvariantCulture));
            return this;            
        }
    }
}