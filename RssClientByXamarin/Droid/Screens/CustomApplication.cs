using System;
using Analytics;
using Android.App;
using Android.Runtime;
using Autofac;
using Shared;

namespace RssClient.Screens
{
	[Application]
    public class CustomApplication : Application
    {
        // TODO вынести его на сервер или типо того
        private const string ApiKeyDebugAndroid = "3a26d323-c850-4b9d-b9bd-f4402dcd9995";
        private const string ApiKeyBattleAndroid = "d6157339-63f5-4c0c-9df1-ffe26b8f851c";

        public CustomApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            App.Build(new ContainerBuilder());

            var log = App.Container.Resolve<ILog>();

#if DEBUG
            {
                log.SetApiKey(ApiKeyDebugAndroid);
            }
#else
{
                log.SetApiKey(ApiKeyBattleAndroid);
}
#endif
        }
    }
}