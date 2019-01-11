using System;
using System.Collections.Generic;

namespace Analytics
{
	public interface ILog
	{
		void TrackEvent(string name, IDictionary<string, string> properties);
		void TrackError(Exception e, IDictionary<string, string> properties);
		void TrackLog(LogLevel logLevel, string tag, string message, Exception e = null);
	}
}
