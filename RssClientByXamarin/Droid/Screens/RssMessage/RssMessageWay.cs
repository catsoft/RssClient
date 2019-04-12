using System;
using Droid.Repository.Configuration;
using Droid.Screens.Navigation;
using Shared.Configuration.Settings;
using Shared.Infrastructure.Navigation;
using Shared.ViewModels.RssMessage;
using Xamarin.Essentials;

namespace Droid.Screens.RssMessage
{
    public class RssMessageWay : IWayWithParameters<RssMessageViewModel, RssMessageParameterses>
    {
        private readonly FragmentActivity _activity;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly RssMessageParameterses _parameters;

        public RssMessageWay(FragmentActivity activity, IConfigurationRepository configurationRepository,
            RssMessageParameterses parameters)
        {
            _activity = activity;
            _configurationRepository = configurationRepository;
            _parameters = parameters;
        }

        public void Go()
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();

            switch (appConfiguration.MessagesViewer)
            {
                case MessagesViewer.Browser:
                    Browser.OpenAsync(_parameters.RssMessageModel.Url);
                    break;
                case MessagesViewer.App:
                    var fragment = new RssMessageFragment(_parameters.RssMessageModel.Id);
                    fragment.SetParameters(_parameters);
                    
                    _activity.AddFragment(fragment);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}