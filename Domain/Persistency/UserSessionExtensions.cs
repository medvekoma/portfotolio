using System.Linq;

namespace Portfotolio.Domain.Persistency
{
    public static class UserSessionExtensions
    {
        public static string[] GetRecommendedUserIds(this IUserSession userSession, string userId)
        {
            string key = DataKeys.RecommendedUserIds + userId;
            var value = userSession.GetValue(key);
            if (value == null)
                return null;
            return value.Split(',');
        }

        public static void SetRecommendedUserIds(this IUserSession userSession, string userId, string[] recommendedUserIds)
        {
            string key = DataKeys.RecommendedUserIds + userId;
            if (recommendedUserIds == null)
            {
                userSession.SetValue(key, null);
                return;
            }

            var aggregate = recommendedUserIds.Aggregate((result, current) => result + ',' + current);
            userSession.SetValue(key, aggregate);
        }
    }
}