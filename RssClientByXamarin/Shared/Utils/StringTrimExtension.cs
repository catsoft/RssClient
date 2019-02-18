namespace Shared.Utils
{
    public static class StringTrimExtension
    {
        public static string SafeTrim(this string str)
        {
            return str?.Trim(' ', '\n', '\r');
        }
    }
}