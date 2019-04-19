#region

using System;
using System.Collections.Generic;

#endregion

namespace Shared.Analytics.Rss
{
    public class ScreenLog
    {
        private readonly ILog _log;

        public ScreenLog(ILog log) { _log = log; }

        public void TrackScreenOpen(Type screen)
        {
            _log.TrackEvent(nameof(TrackScreenOpen), new Dictionary<string, string> {{nameof(screen), screen.Name}});
        }
    }
}
