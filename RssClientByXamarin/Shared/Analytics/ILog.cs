#region

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

#endregion

namespace Shared.Analytics
{
    public interface ILog
    {
        void SetApiKey([NotNull] string apiKey);

        void TrackEvent([CanBeNull] string name, [CanBeNull] IDictionary<string, string> properties);

        void TrackError([CanBeNull] Exception e, [CanBeNull] IDictionary<string, string> properties);

        void TrackLog(LogLevel logLevel, [CanBeNull] string tag, [CanBeNull] string message, Exception e = null);
    }
}
