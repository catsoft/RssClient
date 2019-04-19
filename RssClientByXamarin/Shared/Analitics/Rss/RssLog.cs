#region

using System;
using System.Collections.Generic;
using Shared.Analytics;

#endregion

namespace Shared.Analitics.Rss
{
    public class RssLog
    {
        private readonly ILog _log;

        public RssLog(ILog log) { _log = log; }

        public void TrackRssInsert(string rss, DateTimeOffset time)
        {
            _log.TrackEvent(nameof(TrackRssInsert), new Dictionary<string, string> {{nameof(rss), rss}});
        }

        public void TrackRssUpdate(string oldRss, string newRss, string name, DateTimeOffset time)
        {
            _log.TrackEvent(nameof(TrackRssUpdate),
            new Dictionary<string, string>
            {
                {nameof(oldRss), oldRss},
                {nameof(newRss), newRss},
                {nameof(name), name}
            });
        }

        public void TrackRssDelete(string rss, DateTimeOffset time)
        {
            _log.TrackEvent(nameof(TrackRssDelete), new Dictionary<string, string> {{nameof(rss), rss}});
        }
    }
}
