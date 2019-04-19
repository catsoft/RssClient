#region

using Realms;

#endregion

namespace Shared.Database
{
    public class SettingsModel : RealmObject
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
