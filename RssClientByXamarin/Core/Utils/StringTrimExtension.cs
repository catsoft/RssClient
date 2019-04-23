using JetBrains.Annotations;

namespace Core.Utils
{
    public static class StringTrimExtension
    {
        [CanBeNull]
        public static string SafeTrim([CanBeNull] this string str) { return str?.Trim(' ', '\n', '\r'); }
    }
}
