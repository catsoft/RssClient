using Core.Database;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Core.Repositories.Configurations
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        [NotNull] private readonly SqliteDatabase _sqliteDatabase;

        public ConfigurationRepository([NotNull] SqliteDatabase sqliteDatabase)
        {
            _sqliteDatabase = sqliteDatabase;
        }
        
        public void SaveSetting<T>(T obj)
        {
            _sqliteDatabase.DoWithConnection((connection) =>
            {
                var key = typeof(T).FullName;
                var value = JsonConvert.SerializeObject(obj);
                var item = connection.Table<SettingsModel>()?.FirstOrDefault(w => w.Key == key) ?? new SettingsModel(key);
                item.JsonValue = value;

                connection.InsertOrReplace(item);
            });
        }

        public T GetSettings<T>()
            where T : class, new()
        {
            return _sqliteDatabase.DoWithConnection((connection) =>
            {
                var key = typeof(T).FullName;
                var item = connection.Table<SettingsModel>()?.FirstOrDefault(w => w.Key == key);
                return item == null ? new T() : JsonConvert.DeserializeObject<T>(item.JsonValue) ?? new T();
            });
        }

        public void DeleteSetting<T>()
        {
            _sqliteDatabase.DoWithConnection((connection) =>
            {
                var key = typeof(T).FullName;
                var item = connection.Table<SettingsModel>()?.FirstOrDefault(w => w.Key == key);
                if (item != null) connection.Delete(item);
            });
        }
    }
}
