using System;
using System.Collections.Generic;
using System.Drawing;

namespace BAS.ConfigUtil.Test
{
    class TestModel
    {
        public enum testEnum
        {
            Item1,
            Item2,
            Item3
        }

        [ConfigProp(10)]
        public int IntProp { get; set; }

        [ConfigProp("Int2", 10)]
        public int IntProp2 { get; set; }

        [ConfigProp("Hello")]
        public string StringProp { get; set; }

        [ConfigProp(true)]
        public bool BoolProp { get; set; }

        [ConfigProp(testEnum.Item1)]
        public testEnum EnumProp { get; set; }

        [ConfigProp("2015/1/1 12:3:4")]
        public DateTime DateProp { get; set; }


        [ConfigProp("1.2:30:45")]
        public TimeSpan TimespanProp { get; set; }

        [ConfigProp("Black")]
        public Color ColorProp { get; set; }

        [ConfigProp("#00FF00")]
        public Color ColorProp2 { get; set; }

        [ConfigProp("1,2,10,900")]
        public List<int> IntList { get; set; }

        [ConfigProp("2015/1/1 12:3:4,2015/2/10 14:45:04,2015/3/11 01:3:4,")]
        public List<DateTime> DateTimeList { get; set; }
    }
}
