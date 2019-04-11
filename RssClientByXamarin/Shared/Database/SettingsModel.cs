using Realms;

namespace Shared.Database
{
    public class SettingsModel : RealmObject
    {
        public string Key { get; set; }
        
        public string JsonValue { get; set; }

        public SettingsModel()
        {

        }

        public SettingsModel(string key, string jsonValue = null)
        {
            Key = key;
            JsonValue = jsonValue;
        }
    }
}