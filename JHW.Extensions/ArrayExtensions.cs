using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JHW
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// 将字节数组化序列化成对象。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this byte[] bytes) where T : class
        {
            if (null == bytes)
            {
                return default(T);
            }

            using (var stream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();
                try
                {
                    var obj = formatter.Deserialize(stream);
                    return obj as T;
                }
                catch (Exception)
                {
                    return default(T);
                }
            }
        }
    }
}
