using System;
using System.Collections.Generic;

namespace Analytics.Rss
{
    public class ScreenLog
    {
        private readonly ILog _log;

        private ScreenLog(ILog log)
        {
            _log = log;
        }

        public void TrackScreenOpen(Type screen)
        {
            _log.TrackEvent(nameof(TrackScreenOpen), new Dictionary<string, string>()
            {
                {nameof(screen), screen.Name},
            });
        }
    }
}
