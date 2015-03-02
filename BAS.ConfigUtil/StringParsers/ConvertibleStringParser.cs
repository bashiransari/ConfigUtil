using System;


namespace BAS.ConfigUtil.StringParsers
{
    public class ConvertibleStringParser : BaseStringParser
    {
        #region BaseStringParser Methods
        public override bool CanParseToType(Type type)
        {
            return IsConvertibleType(type);
        }

        public override object Parse(string value, Type type)
        {
            var result = Convert.ChangeType(value, type);
            return result;
        }
        #endregion

        #region Private Methods
        bool IsConvertibleType(Type type)
        {
            switch (type.Name)
            {
                case "Boolean":
                case "Byte":
                case "Char":
                case "DateTime":
                case "Decimal":
                case "Double":
                case "Short":
                case "Int32":
                case "Int64":
                case "SByte":
                case "Single":
                case "String":
                case "Object":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    return true;
                    break;
            }
            return false;
        }

        #endregion
    }
}
