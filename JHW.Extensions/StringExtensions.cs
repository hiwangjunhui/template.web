using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace JHW
{
    public static class StringExtensions
    {
        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Deseliraizlie<T>(this string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将字符串转为byte数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMD5String(this string value)
        {
            var bytes = value.ToBytes();
            if (null == bytes)
            {
                return null;
            }

            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(bytes);
                var sb = new StringBuilder(bytes.Length);
                Array.ForEach(hash, b => sb.Append(b.ToString("X2")));
                return sb.ToString();
            }
        }
    }
}
