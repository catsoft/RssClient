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

        public Task<RssData> Find(string id, CancellationToken cancellationToken = default) => _rssRepository.Find(id);

        public Task Update(string id, string value, CancellationToken cancellationToken = default) =>
            _rssRepository.Update(id, value, cancellationToken);
    }
}