using System;

namespace BAS.ConfigUtil
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ConfigProp : Attribute
    {
        #region Properties
        public string Name
        {
            get;
            set;
        }
        public object DefaultValue
        {
            get;
            set;
        }

        #endregion

        #region Ctors
        public ConfigProp(string name, object defaultvalue)
        {
            this.DefaultValue = defaultvalue;
            this.Name = name;
        }

        public ConfigProp(object defaultvalue)
    :this("",defaultvalue)
        {
            
        }

        public ConfigProp()
        {

        }
        #endregion
    }

}