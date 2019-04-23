using Core.Infrastructure.ViewModels;
using Core.Services.RssMessages;

namespace Core.ViewModels.Messages.Message
{
    public class MessageParameters : ViewModelParameters
    {
        public MessageParameters(RssMessageServiceModel rssMessageModel) { RssMessageModel = rssMessageModel; }

        public RssMessageServiceModel RssMessageModel { get; }
    }
}
