using System;
using System.Threading.Tasks;

namespace JHW.ICache
{
    public interface ICache : IDisposable
    {
        bool Set<T>(string key, T value, TimeSpan expiresIn);

        Task<bool> SetAsync<T>(string key, T value, TimeSpan expiresIn);

        bool Remove(string key);

        Task<bool> RemoveAsync(string key);

        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        bool ExpireIn(string key, TimeSpan expireIn);

        Task<bool> ExpireInAsync(string key, TimeSpan expireIn);
    }
}
