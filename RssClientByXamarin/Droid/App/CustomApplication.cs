using System;
using Analytics;
using Android.App;
using Android.Content;
using Android.Runtime;
using Shared.App.Rss;

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

            Log.Init();

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

	            var repository = RssRepository.Instance;

	            repository.Insert("https://meteoinfo.ru/rss/forecasts/index.php?s=28440");
	            repository.Insert("https://acomics.ru/~depth-of-delusion/rss");
	            repository.Insert("http://www.calend.ru/img/export/calend.rss");
	            repository.Insert("http://www.old-hard.ru/rss");
            }
        }
    }
}