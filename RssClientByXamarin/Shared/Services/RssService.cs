using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Shared.Repository.Rss;

namespace Shared.Services
{
    public class RssService : IRssService
    {
        private readonly IRssRepository _rssRepository;

        public RssService(IRssRepository rssRepository)
        {
            _rssRepository = rssRepository;
        }

        public Task Create(string url, CancellationToken cancellationToken = default) =>
            _rssRepository.InsertByUrl(url, cancellationToken);

        public Task<RssData> Find(string id, CancellationToken cancellationToken = default) => _rssRepository.Find(id);

        public Task Remove(string id, CancellationToken token = default) => _rssRepository.Remove(id, token);

        public Task<IEnumerable<RssData>> GetList(CancellationToken token = default) => _rssRepository.GetList(token);

        public Task Update(string rssId, SyndicationFeed feed, CancellationToken token = default) =>
            _rssRepository.Update(rssId, feed, token);

        public Task UpdatePosition(string id, int position, CancellationToken token = default) =>
            _rssRepository.UpdatePosition(id, position, token);

        public Task ReadAllMessages(string id, CancellationToken token = default) =>
            _rssRepository.ReadAllMessages(id, token);

        public Task Update(string id, string value, CancellationToken cancellationToken = default) =>
            _rssRepository.Update(id, value, cancellationToken);
    }
}