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
    public class ConfigReader
    {
        public string Prefix { get; set; }
        readonly ILog Logger;
        public ConfigReader(string prefix)
        {
            //this.Logger = LogManager.GetLogger("CfgReader");
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

                    //if (tracevalues)
                    //    this.Logger.TraceFormat("{0} = {1}", configName, temp);

                    object propValue;
                    if (StringParser.TryParse(temp, prop.PropertyType, out propValue))
                    {
                        prop.SetValue(theobject, propValue, null);
                    }
                    else
                    {
                        this.Logger.WarnFormat("Invalid configuration value for \"{0}\" , Value:{1}", prop.Name, temp);
                    }

                }
            }
            catch (Exception ex)
            {
                this.Logger.WarnFormat("cannot load configuration for {0}", ex, type.ToString());
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
                    else if (defaultValue.GetType() == typeof(string))
                    {
                        object propValue;
                        if (StringParser.TryParse(defaultValue.ToString(),
                            property.PropertyType, out propValue))
                        {
                            property.SetValue(theObject, propValue, null);
                        }
                        else
                        {
                            this.Logger.WarnFormat("Invalid configuration value for \"{0}\" , Value:{1}",
                                property.Name, defaultValue);
                        }
                    }
                    else
                    {
                        throw new InvalidCastException(string.Format("Cannot cast Defaultvalue for {0} property to {1}", property.Name, property.PropertyType.FullName));
                    }
                }
            }
            catch (Exception)
            {
                this.Logger.WarnFormat("Invalid default value for \"{0}\" , Value:{1}", property.Name, defaultValue);
            }
            return true;
        }

    }
}
