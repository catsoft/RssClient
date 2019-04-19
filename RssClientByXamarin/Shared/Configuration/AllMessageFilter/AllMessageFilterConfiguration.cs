#region

using System;
using System.Linq;
using JetBrains.Annotations;
using Shared.Database.Rss;

#endregion

namespace Shared.Configuration.Settings
{
    public class AllMessageFilterConfiguration
    {
        public Sort Sort { get; set; } = Sort.Newest;

        public MessageFilterType MessageFilterType { get; set; } = MessageFilterType.None;

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        [NotNull]
        [ItemCanBeNull]
        public IQueryable<RssMessageModel> ApplySort([NotNull] IQueryable<RssMessageModel> messages)
        {
            switch (Sort)
            {
                case Sort.Oldest:
                    return messages.OrderBy(w => w.CreationDate);
                case Sort.Newest:
                    return messages.OrderByDescending(w => w.CreationDate);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull]
        [ItemCanBeNull]
        public IQueryable<RssMessageModel> ApplyFilter([NotNull] IQueryable<RssMessageModel> messages)
        {
            var filterMessages = messages;

            switch (MessageFilterType)
            {
                case MessageFilterType.None:
                    return filterMessages;
                case MessageFilterType.Favorite:
                    return filterMessages.Where(w => w.IsFavorite);
                case MessageFilterType.Read:
                    return filterMessages.Where(w => w.IsRead);
                case MessageFilterType.Unread:
                    return filterMessages.Where(w => !w.IsRead);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull]
        [ItemCanBeNull]
        public IQueryable<RssMessageModel> ApplyDateFilter([NotNull] IQueryable<RssMessageModel> messages)
        {
            var filterMessages = messages;

            if (From.HasValue)
            {
                var fromDate = From.Value;
                filterMessages = filterMessages.Where(w => w.CreationDate >= fromDate);
            }

            if (To.HasValue)
            {
                var toDate = To.Value;
                filterMessages = filterMessages.Where(w => w.CreationDate <= toDate);
            }

            return filterMessages;
        }
    }
}
