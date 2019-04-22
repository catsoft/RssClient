using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Shared.Analytics.Rss
{
    public class ScreenLog
    {
        [NotNull] private readonly ILog _log;

        public ScreenLog([NotNull] ILog log) { _log = log; }

        public void TrackScreenOpen([CanBeNull] Type screen)
        {
            _log.TrackEvent(nameof(TrackScreenOpen), new Dictionary<string, string> {{nameof(screen), screen?.Name}});
        }
    }
}
