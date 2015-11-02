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
    public class ConfigReader
    {
        public string Prefix { get; set; }
        public ConfigReader(string prefix)
        {
            this.Prefix = prefix;
        }

        public ConfigReader()
            : this("")
        {

        }

        public void ReadConfig(object theobject, IConfigSource configSource = null)
        {
            if (configSource == null)
                configSource = ConfigSourceFactory.GetDefaultSource();

            Type type = theobject.GetType();

            try
            {
                foreach (Tuple<PropertyInfo, ConfigProp> items in Helper.GetConfigProps(theobject))
                {
                    var prop = items.Item1;
                    var configurationprop = items.Item2;

                    Debug.Assert(configurationprop != null, "configurationprop != null");
                    string configName = configurationprop.Name;

                    if (string.IsNullOrEmpty(configurationprop.Name))
                    {
                        configName = prop.Name;
                    }

                    string temp = null;
                    if (configSource.HasKey(this.Prefix + "." + configName))
                        temp = configSource.GetValue(this.Prefix + "." + configName);
                    else
                    {
                        TrySetDefaultValueForProperty(theobject, configurationprop.DefaultValue, prop);
                        continue;
                    }

                    object propValue;
                    try
                    {
                        propValue = StringParser.Parse(temp, prop.PropertyType);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidCastException(string.Format("Invalid configuration value for \"{0}\" , Value:{1}", prop.Name, temp), ex);
                    }

                    prop.SetValue(theobject, propValue, null);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("cannot load configuration for {0}", ex, type.ToString()), ex);
            }
        }

        private bool TrySetDefaultValueForProperty(object theObject, object defaultValue, PropertyInfo property)
        {
            try
            {
                if (defaultValue != null)
                {
                    if (property.PropertyType == defaultValue.GetType())
                    {
                        property.SetValue(theObject, defaultValue, null);
                    }
                    object propValue;
                    try
                    {
                        propValue = StringParser.Parse(defaultValue.ToString(), property.PropertyType);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidCastException(string.Format("Invalid configuration value for \"{0}\" , Value:{1}", property.Name, defaultValue), ex);
                    }
                    property.SetValue(theObject, propValue, null);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Invalid default value for \"{0}\" , Value:{1}", property.Name, defaultValue), ex);
            }
            return true;
        }

    }
}
