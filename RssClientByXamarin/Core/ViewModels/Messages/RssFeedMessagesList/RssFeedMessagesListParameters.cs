using Core.Infrastructure.ViewModels;
using Core.Services.RssFeeds;
using JetBrains.Annotations;

namespace Core.ViewModels.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesListParameters : ViewModelParameters
    {
        public RssFeedMessagesListParameters([NotNull] RssFeedServiceModel rssFeedModel) { RssFeedModel = rssFeedModel; }

        [NotNull] public RssFeedServiceModel RssFeedModel { get; }
    }
}
