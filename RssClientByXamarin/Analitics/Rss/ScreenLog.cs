using System;
using System.Collections.Generic;

namespace Analytics.Rss
{
    public class ScreenLog
    {
        private static ScreenLog _instance;
        public static ScreenLog Instance => _instance ?? (_instance = new ScreenLog());

        private readonly Analytics.Log _log;

        private ScreenLog()
        {
            _log = Analytics.Log.Instance;
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
