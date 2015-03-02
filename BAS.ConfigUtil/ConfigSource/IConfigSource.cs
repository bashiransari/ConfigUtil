namespace BAS.ConfigUtil.ConfigSource
{
    public interface IConfigSource
    {
        bool HasKey(string key);
        string GetValue(string key);

        void SetValue(string key, string value);
        //bool FlushValues();
        string GetConfigString();
    }
}
