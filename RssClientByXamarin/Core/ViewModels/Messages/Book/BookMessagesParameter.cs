using System;
using Core.Infrastructure.ViewModels;

namespace Core.ViewModels.Messages.Book
{
    public class BookMessagesParameter : ViewModelParameters
    {
        public BookMessagesParameter(Guid rssMessageId, Guid rssFeedId)
        {
            RssMessageId = rssMessageId;
            RssFeedId = rssFeedId;
        }

        public BookMessagesParameter()
        {
        }

        public Guid RssMessageId { get; set; }
        
        public Guid RssFeedId { get; set; }
    }
}
