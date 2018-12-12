using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Shared.Metrics
{
	public static class Log
	{
		public static void TrackEvent(string name, IDictionary<string, string> properties)
		{
			Analytics.TrackEvent(name, properties);
		}

		public static void TrackError(Exception e, IDictionary<string, string> properties)
		{
			Crashes.TrackError(e, properties);
		}
	}
}
