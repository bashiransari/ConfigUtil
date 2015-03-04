using System.Collections.Generic;
using System.Security;
using System.Web.Script.Serialization;
using BAS.ConfigUtil.ConfigSource;

namespace BAS.ConfigUtil
{
    public static class ExtetionMethods
    {
        public static ConfigReader LoadConfiguration(this object theobject, IConfigSource configSource = null, string prefix = "")
        {
            var configReader = new ConfigReader(prefix);
            configReader.ReadConfig(theobject, configSource);
            return configReader;
        }

        public static ConfigReader LoadConfiguration(this object theobject, string prefix = "")
        {
            return LoadConfiguration(theobject, (IConfigSource)null, prefix);
        }

        public static ConfigReader LoadConfiguration(this object theobject, Dictionary<string, string> DictionaryConfigSource, string prefix = "")
        {
            return LoadConfiguration(theobject, new DictionaryConfigSource(DictionaryConfigSource), prefix);
        }

        public static ConfigReader LoadConfiguration(this object theobject, string JsonConfigSource, string prefix = "")
        {
            return LoadConfiguration(theobject, new JsonStringConfigSource(JsonConfigSource), prefix);
        }

        public static bool SaveConfiguration(this object theobject, string prefix = "")
        {
            var configWriter = new ConfigWriter(prefix);
            return configWriter.WriteConfig(theobject, false);
        }

        public static bool SaveDefaultConfiguration(this object theobject, string prefix = "")
        {
            var configWriter = new ConfigWriter(prefix);
            return configWriter.WriteConfig(theobject, true);
        }

        public static string GetConfigurationJson(this object theobject, string prefix = "")
        {
            var jsonConfigSource = new JsonStringConfigSource("{}");
            var configWriter = new ConfigWriter(prefix);
            return configWriter.GetConfigString(theobject, jsonConfigSource);
        }

        public static string GetDefaultConfigurationJson(this object theobject, string prefix = "")
        {
            var jsonConfigSource = new JsonStringConfigSource("{}");
            var configWriter = new ConfigWriter(prefix);
            return configWriter.GetConfigString(theobject, jsonConfigSource, true);
        }
    }
}