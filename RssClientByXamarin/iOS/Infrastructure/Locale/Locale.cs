using Foundation;
using iOS.Locale;

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