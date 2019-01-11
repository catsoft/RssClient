using System.Collections.Generic;

namespace Analytics.Rss
{
    public class RssMessageLog
    {
        private static RssMessageLog _instance;
        public static RssMessageLog Instance => _instance ?? (_instance = new RssMessageLog());

        private readonly Log _log;

        private RssMessageLog()
        {
            _log = Log.Instance;
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
