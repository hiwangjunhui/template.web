using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace JHW.Utilities
{
    public static class Utils
    {
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void SetPropertiesForType<T>()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty);
            Array.ForEach(properties, p =>
            {
                var defaultValue = p.GetCustomAttribute<DefaultValueAttribute>()?.Value;
                if (null != defaultValue)
                {
                    p.SetValue(null, defaultValue);
                }

                var valueOfConfigFile = ConfigurationManager.AppSettings[$"{p.DeclaringType.Name}.{p.Name}"];
                if (!string.IsNullOrEmpty(valueOfConfigFile))
                {
                    p.SetValue(null, Convert.ChangeType(valueOfConfigFile, p.PropertyType));
                }
            });
        }
    }
}
