using System;
using System.Collections.Generic;
using System.Linq;
using BAS.ConfigUtil.StringParsers;

namespace BAS.ConfigUtil
{
    public static class StringParser
    {
        #region Variables
        private static List<IStringParser> _parsers;
        #endregion

        #region Ctors
        static StringParser()
        {
            _parsers = new List<IStringParser>();
            RegisterDefaultParsers();
        }

        private static void RegisterDefaultParsers()
        {
            var interfaceType = typeof(IStringParser);
            var parsers = typeof(StringParser).Assembly.GetTypes()
                .Where(p => !p.IsAbstract &&
                            p.IsClass &&
                            interfaceType.IsAssignableFrom(p)).ToList();

            parsers.ForEach(
                p =>
                {
                    var parser = Activator.CreateInstance(p) as IStringParser;
                    if (parser != null)
                        RegisterParser(parser);
                });
        }

        #endregion

        public static string ListSeprator { get; set; }

        #region Methods
        public static void ClearParsers()
        {
            _parsers.Clear();
        }

        public static void RegisterParser(IStringParser parser, int index = -1)
        {
            if (index == -1)
                _parsers.Add(parser);
            else
            {
                _parsers.Insert(index, parser);
            }
        }

        public static object Parse(string value, Type type)
        {
            var parser = FindParser(type);
            if (parser != null)
            {
                return parser.Parse(value, type);
            }
            else
            {
                throw new InvalidOperationException("Cannot find a valid StringParser for Type: " + type.FullName);
            }
        }

        public static string ToString(object value, Type type)
        {
            var parser = FindParser(type);
            if (parser != null)
            {
                return parser.ToString(value, type);
            }
            else
            {
                throw new InvalidOperationException("Cannot find a valid StringParser for Type: " + type.FullName);
            }
        }

        public static T Parse<T>(string value)
        {
            var destType = typeof(T);
            return (T)Parse(value, destType);
        }

        public static string ToString<T>(T value)
        {
            var destType = typeof(T);
            return ToString(value, destType);
        }

        static IStringParser FindParser(Type type)
        {
            return _parsers.FirstOrDefault(p => p.CanParseToType(type));
        }
        #endregion
    }
}
