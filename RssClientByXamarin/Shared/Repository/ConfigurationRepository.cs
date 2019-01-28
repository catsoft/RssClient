using System.Linq;
using Newtonsoft.Json;
using Shared.Database;

namespace Droid.Repository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly RealmDatabase _database;

        public ConfigurationRepository(RealmDatabase database)
        {
            _database = database;
        }

        public void SaveSetting<T>(T obj)
        {
            var key = typeof(T).FullName;
            var value = JsonConvert.SerializeObject(obj);
            var item = _database.MainThreadRealm.All<SettingsModel>().FirstOrDefault(w => w.Key == key);
            if (item == null)
                item = new SettingsModel(key);
            item.JsonValue = value;
            
            _database.MainThreadRealm.Add(item, true);
        }

        public T GetSettings<T>()
        where T : new()
        {
            var key = typeof(T).FullName;
            var item = _database.MainThreadRealm.All<SettingsModel>().FirstOrDefault(w => w.Key == key);
            return item == null ? new T() : JsonConvert.DeserializeObject<T>(item.JsonValue);
        }
    }
}