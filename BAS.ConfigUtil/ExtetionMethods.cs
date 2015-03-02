using System.Collections.Generic;
using System.Security;
using System.Web.Script.Serialization;
using BAS.ConfigUtil.ConfigSource;

namespace BAS.ConfigUtil
{
    public static class ExtetionMethods
    {
        public static ConfigReader LoadConfiguration(this object theobject, string prefix, IConfigSource configSource, bool tracevalues = false)
        {
            var configReader = new ConfigReader(prefix);
            configReader.ReadConfig(theobject, configSource, tracevalues);
            return configReader;
        }

        public static ConfigReader LoadConfiguration(this object theobject, string prefix, bool tracevalues = false)
        {
            return LoadConfiguration(theobject, prefix, new AppSettingsConfigSource());
        }

        public static ConfigReader LoadConfiguration(this object theobject, string prefix, Dictionary<string, string> DictionaryConfigSource, bool tracevalues = false)
        {
            return LoadConfiguration(theobject, prefix, new DictionaryConfigSource(DictionaryConfigSource), tracevalues);
        }

        public static ConfigReader LoadConfiguration(this object theobject, string prefix, string JsonConfigSource, bool tracevalues = false)
        {
            return LoadConfiguration(theobject, prefix, new JsonConfigSource(JsonConfigSource), tracevalues);
        }

        public static bool SaveConfiguration(this object theobject, string prefix)
        {
            var configWriter = new ConfigWriter(prefix);
            return configWriter.WriteConfig(theobject, false);
        }

        public static bool SaveDefaultConfiguration(this object theobject, string prefix)
        {
            var configWriter = new ConfigWriter(prefix);
            return configWriter.WriteConfig(theobject, true);
        }

        public static string GetConfigurationJson(this object theobject, string prefix)
        {
            var jsonConfigSource = new JsonConfigSource("{}");
            var configWriter = new ConfigWriter(prefix);
            return configWriter.GetConfigString(theobject, jsonConfigSource);
        }

        public static string GetDefaultConfigurationJson(this object theobject, string prefix)
        {
            var jsonConfigSource = new JsonConfigSource("{}");
            var configWriter = new ConfigWriter(prefix);
            return configWriter.GetConfigString(theobject, jsonConfigSource, true);
        }
    }
}