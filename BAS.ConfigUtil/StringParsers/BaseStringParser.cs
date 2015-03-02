using System;
using System.Reflection;

namespace BAS.ConfigUtil.StringParsers
{
    public abstract class BaseStringParser : IStringParser
    {
        #region Methods
        public abstract bool CanParseToType(Type type);

        public virtual bool TryParse<T>(string value, out T output)
        {
            try
            {
                output = Parse<T>(value);
                return true;
            }
            catch (Exception ex)
            {
                output = default(T);
                return false;
            }
        }

        public virtual T Parse<T>(string value)
        {
            return (T)Parse(value, typeof(T));
        }
        #endregion

        public abstract object Parse(string value, Type type);

        public new virtual string ToString(object value, Type type)
        {
            return (value ?? "").ToString();
        }
    }
}
