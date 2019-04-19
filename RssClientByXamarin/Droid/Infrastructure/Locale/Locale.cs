#region

using Shared.Infrastructure.Locale;

#endregion

namespace Droid.Infrastructure.Locale
{
    public class Locale : ILocale
    {
        public string GetCurrentLocaleId()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-");
            return netLanguage.ToLower();
        }
    }
}
