using System;
using System.Linq;
using Shared.Database.Rss;

namespace Shared.Configuration.Settings
{
    public class AllMessageFilterConfiguration
    {
        public Sort Sort { get; set; } = Sort.Newest;

        // Учитывается фильтр только если включен хотя бы один
        public bool IsFavorite { get; set; }
        public bool IsRead { get; set; }
        public bool IsUnread { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        

        public IQueryable<RssMessageModel> ApplySort(IQueryable<RssMessageModel> messages)
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

        public IQueryable<RssMessageModel> ApplyFilter(IQueryable<RssMessageModel> messages)
        {
            var filterMessages = messages;

            if (IsFavorite || IsRead || IsUnread)
            {
                filterMessages = filterMessages.Where(w =>
                    (!IsFavorite || IsFavorite && w.IsFavorite) &&
                    (!IsRead || IsRead && w.IsRead) &&
                    (!IsUnread || IsUnread && !w.IsRead));
            }

            return filterMessages;
        }

        public IQueryable<RssMessageModel> ApplyDateFilter(IQueryable<RssMessageModel> messages)
        {
            var filterMessages = messages;

            if (From.HasValue)
            {
                var fromDate = From.Value;
                filterMessages = messages.Where(w => w.CreationDate >= fromDate);
            }
            
            if (To.HasValue)
            {
                var toDate = To.Value;
                filterMessages = messages.Where(w => w.CreationDate <= toDate);
            }
            
            return filterMessages;
        }
    }
}