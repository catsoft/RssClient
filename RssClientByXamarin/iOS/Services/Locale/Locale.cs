using Foundation;
using Shared.Services.Locale;

namespace iOS.Services.Locale
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