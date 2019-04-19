#region

using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Shared.Database;
using Shared.Extensions;

#endregion

namespace Droid.Repositories.Configuration
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        [NotNull] private readonly RealmDatabase _database;

        public ConfigurationRepository([NotNull] RealmDatabase database) => _database = database;

        public void SaveSetting<T>(T obj)
        {
            RealmDatabase.DoInBackground(realm =>
            {
                var key = typeof(T).FullName;
                var value = JsonConvert.SerializeObject(obj);
                var item = realm.NotNull().All<SettingsModel>()?.FirstOrDefault(w => w.Key == key) ?? new SettingsModel(key);
                item.JsonValue = value;

                realm.NotNull().Add(item, true);
            });
        }

        public T GetSettings<T>()
            where T : class, new()
        {
            var key = typeof(T).FullName;
            var item = _database.MainThreadRealm.All<SettingsModel>()?.FirstOrDefault(w => w.Key == key);
            return item == null ? new T() : (JsonConvert.DeserializeObject<T>(item.JsonValue) ?? new T());
        }

        public void DeleteSetting<T>()
        {
            RealmDatabase.DoInBackground(realm =>
            {
                var key = typeof(T).FullName;
                var item = realm.NotNull().All<SettingsModel>()?.FirstOrDefault(w => w.Key == key);
                if (item != null) realm.NotNull().Remove(item);
            });
        }
    }
}
