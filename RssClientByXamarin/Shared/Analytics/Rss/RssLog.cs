using System.Collections.Generic;
using JetBrains.Annotations;

namespace Shared.Analytics.Rss
{
    public class RssLog
    {
        [NotNull] private readonly ILog _log;

        public RssLog([NotNull] ILog log) { _log = log; }

        public void TrackRssInsert([CanBeNull] string rss)
        {
            _log.TrackEvent(nameof(TrackRssInsert), new Dictionary<string, string> {{nameof(rss), rss}});
        }

        public void TrackRssUpdate([CanBeNull] string oldRss, [CanBeNull] string newRss, [CanBeNull] string name)
        {
            _log.TrackEvent(nameof(TrackRssUpdate),
                new Dictionary<string, string>
                {
                    {nameof(oldRss), oldRss},
                    {nameof(newRss), newRss},
                    {nameof(name), name}
                });
        }

        public void TrackRssDelete([CanBeNull] string rss)
        {
            _log.TrackEvent(nameof(TrackRssDelete), new Dictionary<string, string> {{nameof(rss), rss}});
        }
    }
}
