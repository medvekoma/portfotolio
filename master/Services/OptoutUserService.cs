using System;
using System.Collections.Generic;
using System.Threading;

namespace Services
{
    public class OptoutUserService
    {
        private readonly IOptoutUserStore _optoutUserStore;

        public OptoutUserService(IOptoutUserStore optoutUserStore)
        {
            _optoutUserStore = optoutUserStore;
        }

        private SortedSet<string> _optedOutUserIds = null;
        private readonly ReaderWriterLock _lock = new ReaderWriterLock();
        private const int ReadTimeout = 50;
        private const int WriteTimeout = 1000;

        public IEnumerable<string> GetOptedOutUserIds()
        {
            _lock.AcquireReaderLock(ReadTimeout);
            try
            {
                if (_optedOutUserIds == null)
                {
                    _lock.UpgradeToWriterLock(ReadTimeout);
                    try
                    {
                        _optedOutUserIds = _optoutUserStore.ReadUsers();
                    }
                    finally
                    {
                        _lock.ReleaseWriterLock();
                    }                    
                }
            
                return _optedOutUserIds;
            }
            finally
            {
                if (_lock.IsReaderLockHeld)
                    _lock.ReleaseReaderLock();
            }
        }

        public void AddOptedOutUser(string userId)
        {
            _lock.AcquireWriterLock(WriteTimeout);
            try
            {
                if (_optedOutUserIds == null)
                    _optedOutUserIds = new SortedSet<string>() {userId};
                else
                    _optedOutUserIds.Add(userId);
                _optoutUserStore.WriteUsers(_optedOutUserIds);
            }
            finally
            {
                _lock.ReleaseWriterLock();
            }
        }

        public void RemoveOptedOutUser(string userId)
        {
            _lock.AcquireWriterLock(WriteTimeout);
            try
            {
                if (_optedOutUserIds == null)
                    _optedOutUserIds = new SortedSet<string>();
                else
                    _optedOutUserIds.Remove(userId);
                _optoutUserStore.WriteUsers(_optedOutUserIds);
            }
            finally
            {
                _lock.ReleaseWriterLock();
            }
        }
    }
}
