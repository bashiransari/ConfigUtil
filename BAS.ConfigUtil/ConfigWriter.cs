using System;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
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
        public ConfigWriter(string prefix)
        {
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
                try
                {
                    propValue = StringParser.ToString(propValue, prop.PropertyType);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(String.Format("Cannot convert value for \"{0}\" property to string.", prop.Name), ex);
                }
                configSource.SetValue(this.Prefix + "." + configName, propStrValue);
            }
            return true;
        }
        public bool WriteDefaultConfig(object theobject, IConfigSource configSource)
        {
            return WriteConfig(theobject, configSource, true);
        }
        public bool WriteConfig(object theobject, bool useDefaultValues = false)
        {
            var appSettingSource = ConfigSourceFactory.GetDefaultSource();
            WriteConfig(theobject, appSettingSource, useDefaultValues);

            appSettingSource.FlushValues();
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
