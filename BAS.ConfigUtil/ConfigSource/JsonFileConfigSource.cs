using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace BAS.ConfigUtil.ConfigSource
{
    class JsonFileConfigSource : DictionaryConfigSource
    {
        private string _filePath = "";
        public JsonFileConfigSource(string filePath)
            : base(new Dictionary<string, string>())
        {
            _filePath = filePath;
            var jsonString = File.ReadAllText(filePath);
            var jss = new JavaScriptSerializer();
            base.settingSource = jss.Deserialize<Dictionary<string, string>>(jsonString);
        }

        public override bool FlushValues()
        {
            var jss = new JavaScriptSerializer();
            var jsonString = jss.Serialize(base.settingSource);
            File.WriteAllText(_filePath, jsonString);
            return true;
        }
    }
}
