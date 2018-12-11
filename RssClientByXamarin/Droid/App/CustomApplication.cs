using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Shared.App.Base.Database;
using Shared.App.Rss;
using Shared.AppCenter;

namespace RssClient.App
{
    [Application]
    public class CustomApplication : Application
    {
        private const string PreferenceSampleSettingsName = "sampleSettings";
        private const string PreferenceSampleSettingsIsInitName = "IsInitSamples";

        public CustomApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            AppCenterContainer.Init();

            InitRssSamples();
        }

        private void InitRssSamples()
        {
            var sharedPreference = GetSharedPreferences(PreferenceSampleSettingsName, FileCreationMode.Private);

            if (!sharedPreference.Contains(PreferenceSampleSettingsIsInitName) && !sharedPreference.GetBoolean(PreferenceSampleSettingsIsInitName, false))
            {
                var editor = sharedPreference.Edit();
                editor.PutBoolean(PreferenceSampleSettingsIsInitName, true);
                editor.Commit();

                var localDb = LocalDb.Instance;
                localDb.AddNewItems(new List<RssModel>
                {
                    new RssModel("meteoinfo","https://meteoinfo.ru/rss/forecasts/index.php?s=28440", DateTime.Now),
                    new RssModel("acomics","https://acomics.ru/~depth-of-delusion/rss", DateTime.Now),
                    new RssModel("calend","http://www.calend.ru/img/export/calend.rss", DateTime.Now),
                    new RssModel("old-hard","http://www.old-hard.ru/rss", DateTime.Now),
                });
            }
        }
    }
}