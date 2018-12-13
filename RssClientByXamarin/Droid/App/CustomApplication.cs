using System;
using Analytics;
using Android.App;
using Android.Runtime;

namespace RssClient.App
{
	[Application]
    public class CustomApplication : Application
    {
        public CustomApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Log.Init();
        }
    }
}