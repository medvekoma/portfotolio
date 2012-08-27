﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace Portfotolio.Domain.Configuration
{
    public interface IOptoutUserStore
    {
        SortedSet<string> ReadUsers();
        void WriteUsers(IEnumerable<string> userIds);
    }

    public class OptoutUserStore : IOptoutUserStore
    {
        private readonly IOptoutUserStorePathProvider _optoutUserStorePathProvider;

        public OptoutUserStore(IOptoutUserStorePathProvider optoutUserStorePathProvider)
        {
            _optoutUserStorePathProvider = optoutUserStorePathProvider;
        }

        public SortedSet<string> ReadUsers()
        {
            var fileName = _optoutUserStorePathProvider.GetStorageFileName();
            if (!File.Exists(fileName))
                return null;

            var jsonSerializer = new JavaScriptSerializer();
            using(var streamReader = new StreamReader(fileName))
            {
                var content = streamReader.ReadToEnd();
                var userIdArray = jsonSerializer.Deserialize<string[]>(content);
                return new SortedSet<string>(userIdArray);
            }
        }

        public void WriteUsers(IEnumerable<string> userIds)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var fileName = _optoutUserStorePathProvider.GetStorageFileName();
            var userIdArray = userIds.ToArray();
            var content = jsonSerializer.Serialize(userIdArray);
            using (var streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(content);
            }
        }
    }
}