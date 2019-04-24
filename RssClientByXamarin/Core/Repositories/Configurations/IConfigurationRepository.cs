using JetBrains.Annotations;

namespace Core.Repositories.Configurations
{
    public interface IConfigurationRepository
    {
        void SaveSetting<T>([NotNull] T obj);

        [NotNull]
        T GetSettings<T>() where T : class, new();

        void DeleteSetting<T>();
    }
}
