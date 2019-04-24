
namespace Core.Database
{
    public class SettingsModel
    {
        public SettingsModel() { }

        public SettingsModel(string key, string jsonValue = null)
        {
            Key = key;
            JsonValue = jsonValue;
        }

        public string Key { get; set; }

        public string JsonValue { get; set; }
    }
}
