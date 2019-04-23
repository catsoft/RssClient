using Core.Infrastructure.ViewModels;
using JetBrains.Annotations;

namespace Core.ViewModels.Messages.Message
{
    public class MessageViewModel : ViewModelWithParameter<MessageParameters>
    {
        public MessageViewModel([NotNull] MessageParameters parameters) : base(parameters) { }
    }
}
