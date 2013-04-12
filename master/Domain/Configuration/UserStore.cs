using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

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
            var jsonSerializer = new JavaScriptSerializer();
            var userIdArray = userIds.ToArray();
            var content = jsonSerializer.Serialize(userIdArray);
            using (var streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(content);
            }
        }

        private static HashSet<string> GetStoredUserIds(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            var jsonSerializer = new JavaScriptSerializer();
            using (var streamReader = new StreamReader(fileName))
            {
                var content = streamReader.ReadToEnd();
                var userIdArray = jsonSerializer.Deserialize<string[]>(content) ?? new string[0];
                return new HashSet<string>(userIdArray);
            }
        }
    }
}