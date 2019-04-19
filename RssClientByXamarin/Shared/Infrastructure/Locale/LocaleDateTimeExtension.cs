#region

using System;
using System.Globalization;
using Autofac;
using JetBrains.Annotations;
using Shared.Extensions;

#endregion

namespace Shared.Infrastructure.Locale
{
    public static class LocaleDateTimeExtension
    {
        [NotNull]
        private static ILocale ResolveLocale() { return App.Container.Resolve<ILocale>().NotNull(); }

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
