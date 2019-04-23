using JetBrains.Annotations;

namespace Core.Infrastructure.Locale
{
    public interface ILocale
    {
        [NotNull]
        string GetCurrentLocaleId();
    }
}
