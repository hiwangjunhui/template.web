using System.Collections.Generic;

namespace JHW
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 获取数据的值，如果为null, 返回空数组(非null)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IEnumerable<T> ValueOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection ?? new T[0];
        }
    }
}
