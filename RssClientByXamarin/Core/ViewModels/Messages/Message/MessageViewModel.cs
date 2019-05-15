using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;
using Core.Services.RssMessages;
using Core.ViewModels.Settings;
using JetBrains.Annotations;

namespace Core.ViewModels.Messages.Message
{
    public class MessageViewModel : ViewModelWithParameter<MessageParameters>
    {
        public MessageViewModel([NotNull] IRssMessageService messageService,
            [NotNull] INavigator navigator,
            [NotNull] MessageParameters parameters,
            [NotNull] IConfigurationRepository configurationRepository) : base(parameters)
        {
            MessageItemViewModel = new MessageItemViewModel(messageService, navigator, null, new AppConfigurationViewModel(configurationRepository));
        }

        public MessageItemViewModel MessageItemViewModel { get; }
    }
}