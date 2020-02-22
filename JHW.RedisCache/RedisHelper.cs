using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace JHW.RedisCache
{
    public class RedisHelper : ICache.ICache
    {
        private ConnectionMultiplexer _redis;
        public RedisHelper()
        {
            ConnectRedis();
        }

        public void Dispose()
        {
            _redis?.Close();
            _redis?.Dispose();
        }

        public bool ExpireIn(string key, TimeSpan expireIn)
        {
            return DoAction(db => db.KeyExpire(key, expireIn, CommandFlags.DemandMaster));
        }

        public async Task<bool> ExpireInAsync(string key, TimeSpan expireIn)
        {
            return await DoActionAsync(async db => await db.KeyExpireAsync(key, expireIn, CommandFlags.DemandMaster));
        }

        public T Get<T>(string key)
        {
            return DoAction<T>(db =>
            {
                var data = db.StringGet(key, CommandFlags.PreferSlave);
                return ((string)data).Deseliraizlie<T>();
            });
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await DoActionAsync(async db =>
            {
                var data = await db.StringGetAsync(key, CommandFlags.PreferSlave);
                return ((string)data).Deseliraizlie<T>();
            });
        }

        public bool Remove(string key)
        {
            return DoAction(db => db.KeyDelete(key, CommandFlags.DemandMaster));
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await DoActionAsync(async db => await db.KeyDeleteAsync(key, CommandFlags.DemandMaster));
        }

        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            return DoAction(db =>
            {
                return db.StringSet(key, value.SerializeObject(), expiresIn, flags: CommandFlags.DemandMaster);
            });
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan expiresIn)
        {
            return await DoActionAsync(async db => await db.StringSetAsync(key, value.SerializeObject(), expiresIn, flags: CommandFlags.DemandMaster));
        }

        private T DoAction<T>(Func<IDatabase, T> func)
        {
            if (!ConnectRedis())
            {
                return default(T);
            }

            var db = _redis?.GetDatabase();
            return func(db);
        }

        private async Task<T> DoActionAsync<T>(Func<IDatabase, Task<T>> func)
        {
            if (!ConnectRedis())
            {
                return default(T);
            }

            var db = _redis?.GetDatabase();
            return await func(db);
        }

        private bool ConnectRedis()
        {
            try
            {
                if (!(_redis?.IsConnected ?? false))
                {
                    var options = System.Configuration.ConfigurationManager.GetSection("redisConfig") as ConfigurationOptions;
                    _redis = ConnectionMultiplexer.Connect(options);
                }

                return true;
            }
            catch (Exception)
            {
                //ignored
            }
            return false;
        }
    }
}
