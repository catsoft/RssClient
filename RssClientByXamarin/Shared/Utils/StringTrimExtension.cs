using JetBrains.Annotations;

namespace Shared.Utils
{
    public static class StringTrimExtension
    {
        [CanBeNull]
        public static string SafeTrim([CanBeNull] this string str) { return str?.Trim(' ', '\n', '\r'); }
    }
}
