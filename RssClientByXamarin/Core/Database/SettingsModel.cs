using SQLite;

namespace Core.Database
{
    [Table("Settings")]
    public class SettingsModel
    {
        public SettingsModel() { }

        public SettingsModel(string key, string jsonValue = null)
        {
            Key = key;
            JsonValue = jsonValue;
        }

        [PrimaryKey]
        public string Key { get; set; }

        public string JsonValue { get; set; }
    }
}
