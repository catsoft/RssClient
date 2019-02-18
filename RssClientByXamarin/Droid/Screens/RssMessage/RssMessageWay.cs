using System;
using Droid.Repository.Configuration;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;
using Shared.ViewModels;
using Xamarin.Essentials;

namespace Droid.Screens.RssMessage
{
    public class RssMessageWay : RssMessageViewModel.Way
    {
        private readonly FragmentActivity _activity;
        private readonly IConfigurationRepository _configurationRepository;

        public RssMessageWay(FragmentActivity activity, IConfigurationRepository configurationRepository)
        {
            _activity = activity;
            _configurationRepository = configurationRepository;
        }

        public override void Go()
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            
            switch (appConfiguration.MessagesViewer)
            {
                case MessagesViewer.Browser:
                    Browser.OpenAsync(Data.RssMessageModel.Url);
                    break;
                case MessagesViewer.App:
                    _activity.AddFragment(new RssMessageFragment(Data.RssMessageModel.Id));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}