using System.Collections.Generic;

namespace Shared.Analytics.Rss
{
    public class RssMessageLog
    {
        private readonly ILog _log;

        public RssMessageLog(ILog log)
        {
            _log = log;
        }

        public void TrackMessageDelete(string rssUrl, string idMessage, string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageDelete), new Dictionary<string, string>()
            {
                {nameof(rssUrl), rssUrl},
                {nameof(idMessage), idMessage},
                {nameof(titleMessage), titleMessage},
            });
        }

        public void TrackMessageShare(string rssUrl, string idMessage, string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageShare), new Dictionary<string, string>()
            {
                {nameof(rssUrl), rssUrl},
                {nameof(idMessage), idMessage},
                {nameof(titleMessage), titleMessage},
            });
        }

        public void TrackMessageMarkAsRead(string rssUrl, string idMessage, string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageMarkAsRead), new Dictionary<string, string>()
            {
                {nameof(rssUrl), rssUrl},
                {nameof(idMessage), idMessage},
                {nameof(titleMessage), titleMessage},
            });
        }

        public void TrackMessageReadMore(string rssUrl, string idMessage, string titleMessage)
        {
            _log.TrackEvent(nameof(TrackMessageReadMore), new Dictionary<string, string>()
            {
                {nameof(rssUrl), rssUrl},
                {nameof(idMessage), idMessage},
                {nameof(titleMessage), titleMessage},
            });
        }
    }
}
