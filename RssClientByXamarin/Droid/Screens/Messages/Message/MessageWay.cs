using System;
using Core.Configuration.Settings;
using Core.Infrastructure.Navigation;
using Core.Repositories.Configurations;
using Core.ViewModels.Messages.Message;
using Droid.Screens.Navigation;
using Xamarin.Essentials;

namespace Droid.Screens.Messages.Message
{
    public class MessageWay : IWayWithParameters<MessageViewModel, MessageParameters>
    {
        private readonly IFragmentManager _activity;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly MessageParameters _parameters;

        public MessageWay(
            IFragmentManager activity,
            IConfigurationRepository configurationRepository,
            MessageParameters parameters)
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
                    var fragment = new MessageFragment(_parameters.RssMessageModel.Id);
                    fragment.SetParameters(_parameters);

                    _activity.AddFragment(fragment);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
