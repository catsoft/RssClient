using JetBrains.Annotations;

namespace Shared.Infrastructure.Locale
{
    public interface ILocale
    {
        [NotNull]
        string GetCurrentLocaleId();
    }
}
