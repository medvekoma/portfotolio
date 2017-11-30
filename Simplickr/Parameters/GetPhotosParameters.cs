using System.Globalization;

namespace Simplickr.Parameters
{
    public class GetPhotosParameters : RequestParametersBase<GetPhotosParameters>
    {
        public GetPhotosParameters(string userId)
        {
            ParameterMap.Set("user_id", userId);
        }

        public GetPhotosParameters SafeSearch(SafeSearch safeSearch)
        {
            ParameterMap.Set("safe_search", safeSearch);
            return this;
        }

        public GetPhotosParameters Extras(Extras extras)
        {
            ParameterMap.Set("extras", extras);
            return this;            
        }

        public GetPhotosParameters PerPage(int perPage)
        {
            ParameterMap.Set("per_page", perPage.ToString(CultureInfo.InvariantCulture));
            return this;            
        }

        public GetPhotosParameters Page(int page)
        {
            ParameterMap.Set("page", page.ToString(CultureInfo.InvariantCulture));
            return this;            
        }
    }
}