namespace Droid.Repositories.Configuration
{
    public interface IConfigurationRepository
    {
        void SaveSetting<T>(T obj);

        T GetSettings<T>() where T : new();

        void DeleteSetting<T>();
    }
}
