using System;
using System.Collections.Generic;

namespace Analytics.Rss
{
    public class RssLog
    {
        private static RssLog _instance;
        public static RssLog Instance => _instance ?? (_instance = new RssLog());

        private readonly Analytics.Log _log;

        private RssLog()
        {
            _log = Analytics.Log.Instance;
        }

        public void TrackRssInsert(string rss, DateTimeOffset time)
        {
            _log.TrackEvent(nameof(TrackRssInsert), new Dictionary<string, string>()
            {
                {nameof(rss), rss},
            });
        }

        public void TrackRssUpdate(string oldRss, string newRss, string oldName, string newName, DateTimeOffset time)
        {
            _log.TrackEvent(nameof(TrackRssUpdate), new Dictionary<string, string>()
            {
                {nameof(oldRss), oldRss},
                {nameof(newRss), newRss},
                {nameof(oldName), oldName},
                {nameof(newName), newName},
            });
        }

        public void TrackRssDelete(string rss, DateTimeOffset time)
        {
            _log.TrackEvent(nameof(TrackRssDelete), new Dictionary<string, string>()
            {
                {nameof(rss), rss},
            });
        }
    }
}
