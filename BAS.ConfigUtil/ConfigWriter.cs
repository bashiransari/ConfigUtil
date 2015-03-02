using System;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
//using Common.Logging;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using BAS.ConfigUtil.ConfigSource;

namespace BAS.ConfigUtil
{
    public class ConfigWriter
    {
        public string Prefix { get; set; }
        readonly ILog Logger;
        public ConfigWriter(string prefix)
        {
            //this.Logger = LogManager.GetLogger("CfgReader");
            this.Prefix = prefix;
        }

        public ConfigWriter()
            : this("")
        {

        }

        public bool WriteConfig(object theobject, IConfigSource configSource, bool useDefaultValues = false)
        {
            Type type = theobject.GetType();
            foreach (Tuple<PropertyInfo, ConfigProp> items in Helper.GetConfigProps(theobject))
            {
                var prop = items.Item1;
                var configurationprop = items.Item2;

                string configName = configurationprop.Name;

                if (string.IsNullOrEmpty(configurationprop.Name))
                {
                    configName = prop.Name;
                }

                object propValue = null;
                if (useDefaultValues)
                    propValue = configurationprop.DefaultValue ?? "";
                else
                    propValue = prop.GetValue(theobject, new object[] { }) ?? "";

                string propStrValue = "";
                if (StringParser.TryConvertToString(propValue, prop.PropertyType, out propStrValue))
                {
                    configSource.SetValue(this.Prefix + "." + configName, propStrValue);
                }
                else
                {
                    this.Logger.WarnFormat("Cannot convert value for \"{0}\" property to string.", prop.Name);
                }
            }
            return true;
        }
        public bool WriteDefaultConfig(object theobject, IConfigSource configSource)
        {
            return WriteConfig(theobject, configSource, true);
        }
        public bool WriteConfig(object theobject, bool useDefaultValues = false)
        {
            var appSettingSource = new AppSettingsConfigSource();
            WriteConfig(theobject, appSettingSource, useDefaultValues);

            appSettingSource.config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            return true;
        }

        public bool WriteDefaultConfig(object theobject)
        {
            return WriteConfig(theobject, true);
        }


        public string GetConfigString(object theobject, IConfigSource configSource, bool useDefaultValues = false)
        {
            Type type = theobject.GetType();

            WriteConfig(theobject, configSource, useDefaultValues);
            return configSource.GetConfigString();
        }

    }
}
