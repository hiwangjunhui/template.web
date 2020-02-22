using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Mvc;

namespace JHW.Web.Core
{
    public class CustomSessionStateStoreProviderBase : SessionStateStoreProviderBase
    {
        private int _timeout = 20;//Session默认过期时间

        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            _timeout = timeout;
            return new SessionStateStoreData(new SessionStateItemCollection(), SessionStateUtility.GetSessionStaticObjects(context), timeout);
        }

        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {

        }

        public override void Dispose()
        {

        }

        public override void EndRequest(HttpContext context)
        {

        }

        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return Get(context, id, out locked, out lockAge, out lockId, out actions);
        }

        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return Get(context, id, out locked, out lockAge, out lockId, out actions);
        }

        public override void InitializeRequest(HttpContext context)
        {

        }

        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {

        }

        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            Cache.Remove(id);
        }

        public override void ResetItemTimeout(HttpContext context, string id)
        {

        }

        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                using (var binaryWriter = new System.IO.BinaryWriter(stream))
                {
                    (item?.Items as SessionStateItemCollection)?.Serialize(binaryWriter);
                    Cache.Set(id, stream.ToArray(), TimeSpan.FromMinutes(_timeout));
                }
            }
        }

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return true;
        }

        private SessionStateStoreData Get(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            locked = false;
            lockAge = TimeSpan.Zero;
            lockId = null;
            actions = SessionStateActions.None;
            var bytes = Cache.Get<byte[]>(id);
            ISessionStateItemCollection collection = null;
            if (null != bytes)
            {
                using (var stream = new System.IO.MemoryStream(bytes))
                {
                    using (var reader = new System.IO.BinaryReader(stream))
                    {
                        collection = SessionStateItemCollection.Deserialize(reader);
                    }
                }
            }

            Cache.ExpireIn(id, TimeSpan.FromMinutes(_timeout));
            return new SessionStateStoreData(collection ?? new SessionStateItemCollection(), SessionStateUtility.GetSessionStaticObjects(context), _timeout);
        }

        #region Resolve Cache Service
        private ICache.ICache _cache;
        public ICache.ICache Cache
        {
            get
            {
                if (null == _cache)
                {
                    _cache = ResolveCacheService();
                }

                if (null == _cache)
                {
                    throw new CacheConfigurationErrorException("请检查缓存服务是否注入正确");
                }

                return _cache;
            }
        }

        private ICache.ICache ResolveCacheService()
        {
            var cache = DependencyResolver.Current.GetService<ICache.ICache>();
            return cache;
        } 
        #endregion
    }
}
