namespace Core.Extensions
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNotEmpty(this string text)
        {
            return !string.IsNullOrEmpty(text);
        }
    }
}