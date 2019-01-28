namespace Droid.Repository
{
    public interface IConfigurationRepository
    {
        void SaveSetting<T>(T obj);
        
        T GetSettings<T>() where T : new();
    }
}