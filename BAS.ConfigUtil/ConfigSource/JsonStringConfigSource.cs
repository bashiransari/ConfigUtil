using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace BAS.ConfigUtil.ConfigSource
{
    class JsonStringConfigSource : DictionaryConfigSource
    {
        public JsonStringConfigSource(string JsonString)
            : base(new Dictionary<string, string>())
        {
            var jss = new JavaScriptSerializer();
            base.settingSource = jss.Deserialize<Dictionary<string, string>>(JsonString);
        }

    }
}
