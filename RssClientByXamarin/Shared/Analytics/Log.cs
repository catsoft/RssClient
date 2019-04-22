using System;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;

namespace Shared.Analytics
{
    public class Log : ILog
    {
        public void SetApiKey(string apiKey)
        {
            Microsoft.AppCenter.AppCenter.Start(apiKey, typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Crashes));
        }

        public void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(name, properties);
        }

        public void TrackError(Exception e, IDictionary<string, string> properties = null) { Crashes.TrackError(e, properties); }

        public void TrackLog(LogLevel logLevel, string tag, string message, Exception e = null)
        {
            switch (logLevel)
            {
                case LogLevel.Verbose:
                    AppCenterLog.Verbose(tag, message, e);
                    break;
                case LogLevel.Debug:
                    AppCenterLog.Debug(tag, message, e);
                    break;
                case LogLevel.Info:
                    AppCenterLog.Info(tag, message, e);
                    break;
                case LogLevel.Warn:
                    AppCenterLog.Warn(tag, message, e);
                    break;
                case LogLevel.Error:
                    AppCenterLog.Error(tag, message, e);
                    break;
                case LogLevel.Assert:
                    AppCenterLog.Assert(tag, message, e);
                    break;
                default:
                    AppCenterLog.Info("DefaultTrac: " + tag, message, e);
                    break;
            }
        }
    }
}
