using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CoreUtils.Config
{
    //AppConfig is a wrapper that isolates consuming code from having to deal with the complexities of the
    //Microsoft configuration manager.  One such complexity is the casting of datatypes as they are transferred
    //out of the application configuration flat file to memory.

    public static class AppConfig
    {
        private static Configuration _configuration;

        public static void FindAppConfigOrThrow()
        {
            _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        public static string GetStringOrThrow(string key)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if(!hasValue)
            {
                throw new Exception("No value for key in AppSettings config section");
            }
            return (string) value;
        }

        public static string GetString(string key, ref string defaultValue)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if (!hasValue)
            {
                return defaultValue;
            }
            return (string)value;
        }

        public static List<String> GetStringListOrThrow(string key)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if(!hasValue)
            {
                throw new Exception("No value for key in AppSettings config section");
            }
            var itemsAsString = (string) value;
            return itemsAsString.Split(',').ToList();
        }

        public static List<int> GetIntListOrThrow(string key)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if (!hasValue)
            {
                throw new Exception("No value for key in AppSettings config section");
            }
            var itemsAsString = (string)value;
            List<string> list = itemsAsString.Split(',').ToList();
            return list.Select(item => Convert.ToInt32(item)).ToList();
        }

        public static DateTime GetDateTimeOrThrow(string key)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if (!hasValue)
            {
                throw new Exception("No value for key in AppSettings config section");
            }
            return Convert.ToDateTime((string)value);
        }
        
        public static DateTime GetDateTime(string key, ref DateTime dateTime)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if (!hasValue)
            {
                return dateTime;
            }
            return Convert.ToDateTime((string)value);
        }
        
        public static bool GetBoolOrThrow(string key)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if (!hasValue)
            {
                throw new Exception("No value for key in AppSettings config section");
            }
            return Convert.ToBoolean((string)value);
        }

        public static bool GetBoolOr(string key, bool defaultBoolean)
        {
            object value;
            bool hasValue = TryGetValue(key, out value);
            if (!hasValue)
            {
                return defaultBoolean;
            }
            return Convert.ToBoolean((string)value);
        }

        private static bool TryGetValue(string key, out object value)
        {
            string[] allKeys = _configuration.GetSection("appSettings").CurrentConfiguration.AppSettings.Settings.AllKeys;
            if (allKeys.Any(configKey => configKey == key))
            {
                value = _configuration.GetSection("appSettings").CurrentConfiguration.AppSettings.Settings[key].Value;
                return true;
            }
            value = null;
            return false;
        }
    }
}
