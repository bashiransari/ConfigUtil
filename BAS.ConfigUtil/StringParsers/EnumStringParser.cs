using System;
using System.Reflection;

namespace BAS.ConfigUtil.StringParsers
{
    public class EnumStringParser : BaseStringParser
    {
        #region BaseStringParser Methods
        public override bool CanParseToType(Type type)
        {
            return type.IsEnum;
        }

        public override object Parse(string value, Type type)
        {
            return Enum.Parse(type, value);
        }

        #endregion
    }
}
