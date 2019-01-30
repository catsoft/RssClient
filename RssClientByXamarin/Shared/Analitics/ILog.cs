using System;
using System.Collections.Generic;

namespace Shared.Analytics
{
	public interface ILog
	{
		void SetApiKey(string apiKey);
		void TrackEvent(string name, IDictionary<string, string> properties);
		void TrackError(Exception e, IDictionary<string, string> properties);
		void TrackLog(LogLevel logLevel, string tag, string message, Exception e = null);
	}
}
