using System;
using System.Drawing;
using System.Reflection;
using System.Xml.Schema;

namespace BAS.ConfigUtil.StringParsers
{
    public class ColorStringParser : BaseStringParser
    {
        #region BaseStringParser Methods
        public override bool CanParseToType(Type type)
        {
            return type == typeof(Color);
        }

        public override object Parse(string value, Type type)
        {
            object res;
            if (value.StartsWith("#") && (value.Length == 7 || value.Length == 9))
            {
                res = GetColorFromCode(value);
            }
            else
            {
                res = Color.FromName(value);
            }
            return res;
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

        public override string ToString(object value, Type type)
        {
            Color color = Color.Black;
            if (value is Color)
            {
                color = (Color)value;
            }
            string colorStr = string.Format("#{0}{1}{2}{3}",
                color.A.ToString("X2"),
                color.R.ToString("X2"),
                color.G.ToString("X2"),
                color.B.ToString("X2"));

            return colorStr;
        }
    }
}
