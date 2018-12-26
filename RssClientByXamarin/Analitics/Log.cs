using System;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;

namespace Analytics
{
    public class Log : ILog
    {
        // TODO вынести его на сервер или типо того
        private const string ApiKeyDebugIos = "a7eee657-65a4-4fa8-a318-1b367855334b";
        private const string ApiKeyBattleIos = "9e54385d-f3a3-492d-a350-f538d59d742b";
        private const string ApiKeyDebugAndroid = "3a26d323-c850-4b9d-b9bd-f4402dcd9995";
        private const string ApiKeyBattleAndroid = "d6157339-63f5-4c0c-9df1-ffe26b8f851c";

        public static void Init()
        {
            string apiKey;
#if DEBUG
    #if __ANDROID__
            apiKey = ApiKeyDebugAndroid;
    #else
            apiKey = ApiKeyDebugIos;
    #endif
#else
    #if __ANDROID__
            apiKey = ApiKeyBattleAndroid;
    #else
            apiKey = ApiKeyBattleIos;
    #endif
#endif

            Microsoft.AppCenter.AppCenter.Start(apiKey, typeof(Microsoft.AppCenter.Analytics.Analytics), typeof(Crashes));
        }

        public void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(name, properties);
        }

        public void TrackError(Exception e, IDictionary<string, string> properties = null)
        {
            Crashes.TrackError(e, properties);
        }

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
                    AppCenterLog.Info($"DefaultTrac: " + tag, message, e);
                    break;
            }
        }
    }
}