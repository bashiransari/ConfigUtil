using System.Configuration;
using System.Linq;


namespace BAS.ConfigUtil.ConfigSource
{
    class AppSettingsConfigSource : IConfigSource
    {

        private Configuration _config;
        internal Configuration config
        {
            get
            {
                if (_config == null)
                    _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                return _config;
            }
        }

        public bool HasKey(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(key);
        }

        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public void SetValue(string key, string value)
        {
            if (!HasKey(key))
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;
        }

        public string GetConfigString()
        {
            return "";
        }


        public bool FlushValues()
        {
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            return true;
        }
    }
}
