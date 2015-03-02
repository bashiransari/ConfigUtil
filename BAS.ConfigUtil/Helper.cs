using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BAS.ConfigUtil
{
    internal static class Helper
    {
        #region Methods
        internal static IEnumerable<Tuple<PropertyInfo, ConfigProp>> GetConfigProps(object theObject)
        {
            PropertyInfo[] props = theObject.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Attribute attr = Attribute.GetCustomAttribute(prop, typeof(ConfigProp));
                if (attr != null)
                {
                    var configurationprop = attr as ConfigProp;
                    yield return new Tuple<PropertyInfo, ConfigProp>(prop, configurationprop);
                }
            }
        }
        #endregion
    }
}
