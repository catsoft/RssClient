using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using Shared.Database.Rss;

namespace RssClient.Repository
{
    public interface IRssRepository
    {
        Task StartUpdateAllByInternet(string url, string id);
        Task InsertByUrl(string url);
        Task Update(string id, string rss);
        RssModel Find(string id);
        Task Remove(RssModel item);
        IQueryable<RssModel> GetList();
        Task Update(string rssId, SyndicationFeed feed);
        void UpdatePosition(RssModel model, int position);
        void ReadAllMessages(RssModel holderItem);
    }
}