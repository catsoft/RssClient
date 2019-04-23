using Core.Infrastructure.ViewModels;
using Core.Services.Rss;
using JetBrains.Annotations;

namespace Core.ViewModels.Messages.RssFeedMessagesList
{
    public class RssFeedMessagesListParameters : ViewModelParameters
    {
        public RssFeedMessagesListParameters([NotNull] RssServiceModel rssModel) { RssModel = rssModel; }

        [NotNull] public RssServiceModel RssModel { get; }
    }
}
