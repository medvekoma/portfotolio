using System.Linq;

namespace Portfotolio.Domain.Persistency
{
    public static class UserSessionExtensions
    {
        public static AuthenticationInfo GetAuthenticationInfo(this IUserSession userSession)
        {
            return userSession.GetValue<AuthenticationInfo>(DataKeys.AuthenticationInfo);
        }

        public static void SetAuthenticationInfo(this IUserSession userSession, AuthenticationInfo authenticationInfo)
        {
            userSession.SetValue(DataKeys.AuthenticationInfo, authenticationInfo);
        }

        public static string[] GetRecommendedUserIds(this IUserSession userSession, string userId)
        {
            string key = DataKeys.RecommendedUserIds + userId;
            var value = userSession.GetValue<string>(key);
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