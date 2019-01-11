using Foundation;
using Shared.App.Locale;

namespace iOS.Infrastructure.Locale
{
    public class Locale : ILocale
    {
        public string GetCurrentLocaleId()
        {
            var locale = NSLocale.CurrentLocale.CountryCode;
            return locale.ToLower();
        }
    }
}