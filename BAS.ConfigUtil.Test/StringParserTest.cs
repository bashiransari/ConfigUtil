using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BAS.ConfigUtil.Test
{
    [TestClass]
    public class StringParserTest
    { 
        public enum testEnum
        {
            Item1,
            Item2,
            Item3
        } 

         
        [TestMethod]
        public void TestInt()
        {
            var res = StringParser.Parse<int>("12");
            var strres = StringParser.ToString(res);
        }

        [TestMethod]
        public void TestBool()
        {
            var res = StringParser.Parse<bool>("False");
            var strres = StringParser.ToString(res);
        }

        [TestMethod]
        public void TestDateTime()
        {
            var res = StringParser.Parse<DateTime>("2012/1/1 12:00:00");
            var strres = StringParser.ToString(res);
        }

        [TestMethod]
        public void TestTimeSpan()
        {
            var res = StringParser.Parse<TimeSpan>("12:00:00");
            var strres = StringParser.ToString(res);
        }

      
        [TestMethod]
        public void TestEnum()
        {
            var res = StringParser.Parse<testEnum>("Item1");
            var strres = StringParser.ToString(res);
        }

        [TestMethod]
        public void TestColor()
        {
            var res = StringParser.Parse<Color>("Blue");
            var res1 = StringParser.Parse<Color>("#FF00CC");
            var res2 = StringParser.Parse<Color>("#AAFF00CC");

            var strres = StringParser.ToString(res);
            var strres1 = StringParser.ToString(res1);
            var strres2 = StringParser.ToString(res2);
        }



        [TestMethod]
        public void TestList()
        {
            var res = StringParser.Parse<List<Color>>("Blue,Green,Darkred,Orange");
            var res1 = StringParser.Parse<List<int>>("1,2,3,4565,234");
            var res2 = StringParser.Parse<List<string>>("#AAFF00CC,sala,Tset");

            var strres = StringParser.ToString(res);
            var strres1 = StringParser.ToString(res1);
            var strres2 = StringParser.ToString(res2);
        }

    }
}
