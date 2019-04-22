using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Shared.Extensions
{
    public static class NotNulExtension
    {
        [NotNull]
        public static T NotNull<T>([CanBeNull] this T obj)
            where T : class
        {
            return obj ?? throw new NullException();
        }
    }

    [Serializable]
    public class NullException : Exception
    {
        public NullException() { }

        public NullException(string message) : base(message) { }

        public NullException(string message, Exception inner) : base(message, inner) { }

        protected NullException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
