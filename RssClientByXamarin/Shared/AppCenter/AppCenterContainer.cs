using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Shared.AppCenter
{
    public static class AppCenterContainer
    {
        private const string ApiKey = "3a26d323-c850-4b9d-b9bd-f4402dcd9995";

        public static void Init()
        {
            Microsoft.AppCenter.AppCenter.Start(ApiKey, typeof(Analytics), typeof(Crashes));
        }
    }
}
