#region

using System;
using Android.App;
using Android.Runtime;
using Autofac;
using Droid.Container.Modules;
using Shared;
using Shared.Analytics;
using Shared.Extensions;

#endregion

namespace Droid
{
    [Application]
    public class CustomApplication : Application
    {
        // TODO вынести его на сервер или типо того
        private const string KeyDebugAndroid = "3a26d323-c850-4b9d-b9bd-f4402dcd9995";

        // ReSharper disable once UnusedMember.Local
        private const string KeyBattleAndroid = "d6157339-63f5-4c0c-9df1-ffe26b8f851c";

        public CustomApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();

            App.Build(new PlatformModule());

            var log = App.Container.Resolve<ILog>().NotNull();

#if DEBUG
            {
                log.SetApiKey(KeyDebugAndroid);
            }
#else
{
                log.SetApiKey(KeyBattleAndroid);
}
#endif
        }
    }
}
