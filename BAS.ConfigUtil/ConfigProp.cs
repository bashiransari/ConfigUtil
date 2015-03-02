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

      //  public string ListSeperator
        //{
        //    get;
        //    set;
        //}
        #endregion

        #region Ctors
        public ConfigProp(string name, object defaultvalue)//, char listSepratorChar)
        {
            this.DefaultValue = defaultvalue;
            this.Name = name;
            //this.ListSeperator = listSepratorChar.ToString();
        }

        //public ConfigProp(object defaultvalue, char listSepratorChar)
        //    : this("", defaultvalue, listSepratorChar)
        //{

        //}

        public ConfigProp(object defaultvalue)
    :this("",defaultvalue)
        {
            
        }

        //public ConfigProp(string name, object defaultvalue)
        //    : this(name, defaultvalue, ',')
        //{

        //}

        public ConfigProp()
        {

        }
        #endregion
    }

}