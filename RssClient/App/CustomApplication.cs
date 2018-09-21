using System;
using Android.App;
using Android.Runtime;
using Shared.AppCenter;

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

            AppCenterContainer.Init();
        }
    }
}