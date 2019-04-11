using Android.Content;
using Android.Net;

namespace Droid.Services
{
    public static class RateExtension
    {
        public static void RateInMarket(this Context context)
        {
            var rateIntent = new Intent(Intent.ActionView, Uri.Parse("market://details?id=" + context.PackageName)); 
            context.StartActivity(rateIntent);
        }
    }
}