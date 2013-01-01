using System.Globalization;

namespace Simplickr.Parameters
{
    public class GetPhotosParameters : IRequestParameters
    {
        public ParameterMap ParameterMap { get; private set; }

        public GetPhotosParameters(string userId)
        {
            ParameterMap = new ParameterMap
                {
                    {"user_id", userId}
                };
        }

        public GetPhotosParameters SafeSearch(SafeSearch safeSearch)
        {
            ParameterMap.Add("safe_search", safeSearch);
            return this;
        }

        public GetPhotosParameters Extras(Extras extras)
        {
            ParameterMap.Add("extras", extras);
            return this;            
        }

        public GetPhotosParameters PerPage(int perPage)
        {
            ParameterMap.Add("per_page", perPage.ToString(CultureInfo.InvariantCulture));
            return this;            
        }

        public GetPhotosParameters Page(int page)
        {
            ParameterMap.Add("page", page.ToString(CultureInfo.InvariantCulture));
            return this;            
        }
    }
}