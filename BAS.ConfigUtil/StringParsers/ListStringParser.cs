using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace BAS.ConfigUtil.StringParsers
{
    public class ListStringParser : BaseStringParser
    {
        #region BaseStringParser Methods
        public override bool CanParseToType(Type type)
        {
            return type.IsGenericType
                   && type == typeof(List<>).MakeGenericType(type.GetGenericArguments());
        }

        public override object Parse(string value, Type type)
        {
            var seprator = ",";
            var items = value.Split(new string[] { seprator }, StringSplitOptions.RemoveEmptyEntries);
            var itemsType = type.GetGenericArguments()[0];

            var list = this.GetType().GetMethod("CreateList").MakeGenericMethod(itemsType).Invoke(this, new object[] { items });
            return list;
        }

        public List<T> CreateList<T>(string[] items)
        {
            List<T> list = new List<T>();
            T listitem;
            foreach (var item in items)
            {
                list.Add(StringParser.Parse<T>(item));
            }
            return list;
        }

        public override string ToString(object value, Type type)
        {
            var listStr = new StringBuilder();
            var list = value as IEnumerable ?? new List<string>();

            foreach (var item in list)
            {
                listStr.Append(StringParser.ToString(item, type));
                listStr.Append(",");
            }
            return listStr.ToString();
        }

        #endregion


        #region Private Methods
        object GetColorFromCode(string colorCode)
        {
            colorCode = colorCode.Remove(0, 1);

            int[] colors = new int[] { 0XFF, 0XFF, 0XFF, 0XFF };

            int colorIndex = 3;
            while (colorCode.Length > 0)
            {
                var hexValue = colorCode.Substring(colorCode.Length - 2, 2);
                colors[colorIndex--] = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
                colorCode = colorCode.Remove(colorCode.Length - 2, 2);
            }
            return Color.FromArgb(colors[0], colors[1], colors[2], colors[3]);
        }
        #endregion
    }
}
