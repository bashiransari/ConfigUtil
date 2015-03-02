using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace BAS.ConfigUtil.ConfigSource
{
    internal class DictionaryConfigSource : IConfigSource
    {
        protected Dictionary<string, string> settingSource;

        public DictionaryConfigSource(Dictionary<string, string> SettingSource)
        {
            settingSource = SettingSource;
        }

        public bool HasKey(string key)
        {
            return settingSource.ContainsKey(key);
        }

        public string GetValue(string key)
        {
            return settingSource[key];
        }

        public void SetValue(string key, string value)
        {
            settingSource[key] = value;
        }

        public string GetConfigString()
        {
            var jss = new JavaScriptSerializer();
            return jss.Serialize(settingSource);
        }
    }
}
