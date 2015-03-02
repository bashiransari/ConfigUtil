using System;

namespace BAS.ConfigUtil.StringParsers
{
    public interface IStringParser
    {
        bool CanParseToType(Type type);
        T Parse<T>(string value);
        object Parse(string value, Type type);
        bool TryParse<T>(string value, out T output);
        string ToString(object value, Type type);
    }
}
