using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace Shared.Repository.Rss
{
    public interface IRssRepository
    {
        Task StartUpdateAllByInternet(string url, string id);
        Task InsertByUrl(string url);
        Task Update(string id, string rss);
        RssData Find(string id);
        Task Remove(string id);
        IEnumerable<RssData> GetList();
        Task Update(string rssId, SyndicationFeed feed);
        void UpdatePosition(string id, int position);
        void ReadAllMessages(string id);
    }
}