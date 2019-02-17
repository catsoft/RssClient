using System;
using System.Globalization;
using Autofac;
using Shared.Services.Locale;

namespace Shared.Services.Locale
{
    public static class LocaleDateTimeExtension
    {
        private static ILocale ResolveLocale()
        {
            return App.Container.Resolve<ILocale>();
        }
        
        public static string ToShortDateLocaleString(this DateTime date)
        {
            return date.ToString("d", new CultureInfo(ResolveLocale().GetCurrentLocaleId()));
        }
        
        public static string ToShortDateLocaleString(this DateTimeOffset date)
        {
            return date.ToString("d", new CultureInfo(ResolveLocale().GetCurrentLocaleId()));
        }
        
        public static string ToShortGeneralLocaleString(this DateTime date)
        {
            return date.ToString("g", new CultureInfo(ResolveLocale().GetCurrentLocaleId()));
        }
        
        public static string ToShortGeneralLocaleString(this DateTimeOffset date)
        {
            return date.ToString("g", new CultureInfo(ResolveLocale().GetCurrentLocaleId()));
        }
    }
}