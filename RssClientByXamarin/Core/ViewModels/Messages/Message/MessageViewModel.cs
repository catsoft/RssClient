using Core.Infrastructure.Navigation;
using Core.Infrastructure.ViewModels;
using Core.Services.RssMessages;
using JetBrains.Annotations;

namespace Core.ViewModels.Messages.Message
{
    public class MessageViewModel : ViewModelWithParameter<MessageParameters>
    {
        public MessageViewModel([NotNull] IRssMessageService messageService, 
            [NotNull] INavigator navigator,
            [NotNull] MessageParameters parameters) : base(parameters)
        {
            MessageItemViewModel = new MessageItemViewModel(messageService, navigator, null);
        }

        public MessageItemViewModel MessageItemViewModel { get; }
    }
}