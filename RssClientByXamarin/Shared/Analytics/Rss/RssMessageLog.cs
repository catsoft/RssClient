using System.Collections.Generic;
using JetBrains.Annotations;

namespace Shared.Analytics.Rss
{
    public class RssMessageLog
    {
        [NotNull] private readonly ILog _log;

        public RssMessageLog([NotNull] ILog log) { _log = log; }

        public void TrackMessageDelete([CanBeNull] string rssUrl, [CanBeNull] string idMessage, [CanBeNull] string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageDelete),
                new Dictionary<string, string>
                {
                    {nameof(rssUrl), rssUrl},
                    {nameof(idMessage), idMessage},
                    {nameof(titleMessage), titleMessage}
                });
        }

        public void TrackMessageShare([CanBeNull] string rssUrl, [CanBeNull] string idMessage, [CanBeNull] string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageShare),
                new Dictionary<string, string>
                {
                    {nameof(rssUrl), rssUrl},
                    {nameof(idMessage), idMessage},
                    {nameof(titleMessage), titleMessage}
                });
        }

        public void TrackMessageMarkAsRead([CanBeNull] string rssUrl, [CanBeNull] string idMessage, [CanBeNull] string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageMarkAsRead),
                new Dictionary<string, string>
                {
                    {nameof(rssUrl), rssUrl},
                    {nameof(idMessage), idMessage},
                    {nameof(titleMessage), titleMessage}
                });
        }

        public void TrackMessageReadMore([CanBeNull] string rssUrl, [CanBeNull] string idMessage, [CanBeNull] string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageReadMore),
                new Dictionary<string, string>
                {
                    {nameof(rssUrl), rssUrl},
                    {nameof(idMessage), idMessage},
                    {nameof(titleMessage), titleMessage}
                });
        }
    }
}
