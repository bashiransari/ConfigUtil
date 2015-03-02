using System;
using System.Reflection;

namespace BAS.ConfigUtil.StringParsers
{
    public class ParseMethodStringParser : BaseStringParser
    {
        #region BaseStringParser Methods
        public override bool CanParseToType(Type type)
        {
            return HasParseMethod(type);
        }

        public override object Parse(string value, Type type)
        {
            var parseMethod = GetParseMethod(type);

            if (parseMethod == null)
                throw new InvalidOperationException("There is no Parse method with 1 string parameter is type " + type.FullName);

            var res = parseMethod.Invoke(null, new object[] { value });
            return res;
        }
        #endregion

        #region Private Methods
        MethodInfo GetParseMethod(Type type)
        {
            var parseMethod = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy, null, new Type[] { typeof(string) }, null);
            return parseMethod;
        }

        bool HasParseMethod(Type type)
        {
            var parseMethod = GetParseMethod(type);
            return (parseMethod != null);
        }
        #endregion
    }
}
