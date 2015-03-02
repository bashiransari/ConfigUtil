using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BAS.ConfigUtil.Test
{
    [TestClass]
    public class ConfigReaderTest
    {
        private TestModel model;
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
        public void Start()
        {
            model = new TestModel();
        }

        [TestMethod]
        public void LoadConfig()
        {
            model.LoadConfiguration("Test.");
        }

        [TestMethod]
        public void SaveConfig()
        {
            model.SaveConfiguration("Test");

        }

        [TestMethod]
        public void SaveDefaultConfig()
        {
           model.SaveDefaultConfiguration("Test");
        }

        [TestMethod]
        public void LoadJsonConfig()
        {
            string JsonConfig=
                    @"{
                        Test.IntProp:'43',
                        Test.Int2:'123',
                        Test.StringProp:'Hello Json',
                        Test.BoolProp:'True',
                        Test.EnumProp:'Item2',
                        Test.DateProp:'2014/1/2 15:10:20',
                        Test.TimespanProp:'1:02:30',
                        Test.ColorProp:'#FF00CC',
                        Test.ColorProp2:'Red',
                        Test.IntList:'1,2,34,567',
                        Test.DateTimeList:''
                    }";
            model.LoadConfiguration("Test", JsonConfig);
            //string json = this.GetConfigurationJson("Test");
        }

        [TestMethod]
        public void GetJsonConfig()
        {
            string json = model.GetConfigurationJson("Test");
        }

        [TestMethod]
        public void GetDefaultJsonConfig()
        {
            string json = model.GetDefaultConfigurationJson("Test");
        }

     
    }
}
