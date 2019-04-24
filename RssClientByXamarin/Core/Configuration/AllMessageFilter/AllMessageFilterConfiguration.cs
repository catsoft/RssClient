using System;
using System.Collections.Generic;
using System.Linq;
using Core.Database.Rss;
using Core.Extensions;
using JetBrains.Annotations;

namespace Core.Configuration.AllMessageFilter
{
    public class AllMessageFilterConfiguration
    {
        public Sort Sort { get; set; } = Sort.Newest;

        public MessageFilterType MessageFilterType { get; set; } = MessageFilterType.None;

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        [NotNull]
        [ItemCanBeNull]
        public IEnumerable<RssMessageModel> ApplySort([NotNull] IEnumerable<RssMessageModel> messages)
        {
            switch (Sort)
            {
                case Sort.Oldest:
                    return messages.OrderBy(w => w.NotNull().CreationDate);
                case Sort.Newest:
                    return messages.OrderByDescending(w => w.NotNull().CreationDate);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull]
        [ItemCanBeNull]
        public IEnumerable<RssMessageModel> ApplyFilter([NotNull] IEnumerable<RssMessageModel> messages)
        {
            var filterMessages = messages;

            switch (MessageFilterType)
            {
                case MessageFilterType.None:
                    return filterMessages;
                case MessageFilterType.Favorite:
                    return filterMessages.Where(w => w.NotNull().IsFavorite);
                case MessageFilterType.Read:
                    return filterMessages.Where(w => w.NotNull().IsRead);
                case MessageFilterType.Unread:
                    return filterMessages.Where(w => !w.NotNull().IsRead);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull]
        [ItemCanBeNull]
        public IEnumerable<RssMessageModel> ApplyDateFilter([NotNull] IEnumerable<RssMessageModel> messages)
        {
            var filterMessages = messages;

            if (From.HasValue)
            {
                var fromDate = From.Value;
                filterMessages = filterMessages.Where(w => w.NotNull().CreationDate >= fromDate);
            }

            if (To.HasValue)
            {
                var toDate = To.Value;
                filterMessages = filterMessages.Where(w => w.NotNull().CreationDate <= toDate);
            }

            return filterMessages;
        }
    }
}
