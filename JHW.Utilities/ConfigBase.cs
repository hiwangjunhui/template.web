using System.ComponentModel;

namespace JHW.Utilities
{
    public class ConfigBase
    {
        static ConfigBase()
        {
            Utils.SetPropertiesForType<ConfigBase>();
        }

        [DefaultValue("CurrentUser")]
        public static string CurrentUserSessionKey { get; set; }

        [DefaultValue(true)]
        public static bool EnableCache { get; set; }
    }
}
