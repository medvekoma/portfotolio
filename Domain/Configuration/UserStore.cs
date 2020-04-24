using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Portfotolio.Domain.Configuration
{
    public interface IUserStore
    {
        HashSet<string> ReadOptoutUsers();
        void WriteOptoutUsers(IEnumerable<string> userIds);
        HashSet<string> ReadOptinUsers();
        void WriteOptinUsers(IEnumerable<string> userIds);
    }

    public class UserStore : IUserStore
    {
        private readonly IUserStorePathProvider _userStorePathProvider;

        public UserStore(IUserStorePathProvider userStorePathProvider)
        {
            _userStorePathProvider = userStorePathProvider;
        }

        public HashSet<string> ReadOptoutUsers()
        {
            var fileName = _userStorePathProvider.GetOptoutFileName();
            return GetStoredUserIds(fileName);
        }

        public void WriteOptoutUsers(IEnumerable<string> userIds)
        {
            var fileName = _userStorePathProvider.GetOptoutFileName();
            WriteUserIds(userIds, fileName);
        }

        public HashSet<string> ReadOptinUsers()
        {
            var fileName = _userStorePathProvider.GetOptinFileName();
            return GetStoredUserIds(fileName);
        }

        public void WriteOptinUsers(IEnumerable<string> userIds)
        {
            var fileName = _userStorePathProvider.GetOptinFileName();
            WriteUserIds(userIds, fileName);
        }

        private static void WriteUserIds(IEnumerable<string> userIds, string fileName)
        {
            var userIdArray = userIds.ToArray();
            var content = JsonConvert.SerializeObject(userIdArray);
            using (var streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(content);
            }
        }

        private static HashSet<string> GetStoredUserIds(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            using (var streamReader = new StreamReader(fileName))
            {
                var content = streamReader.ReadToEnd();
                var userIdArray = JsonConvert.DeserializeObject<string[]>(content) ?? new string[0];
                return new HashSet<string>(userIdArray);
            }
        }
    }
}