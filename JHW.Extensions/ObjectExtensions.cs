using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JHW
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 调用 Convert.ChangeType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ChangeType<T>(this object value)
        {
            try
            {
                if (null == value)
                {
                    return default(T);
                }

                if (typeof(T) == value.GetType())
                {
                    return (T)value;
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(this object obj)
        {
            //obj为空时，该方法不抛异常
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将单个对象转换为IEnumerable<T>类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsIEnumerable<T>(this T obj)
        {
            yield return obj;
        }
    }
}
