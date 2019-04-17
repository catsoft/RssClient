using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Infrastructure.Mappers;
using Shared.Repository.Rss;

namespace Shared.Services.Rss
{
    public class RssService : IRssService
    {
        private readonly IRssRepository _rssRepository;
        private readonly IMapper<RssServiceModel, RssDomainModel> _mapper;
        private readonly IMapper<RssDomainModel, RssServiceModel> _toServiceModelMapper;

        public RssService(IRssRepository rssRepository, IMapper<RssServiceModel, RssDomainModel> mapper,
            IMapper<RssDomainModel, RssServiceModel> toServiceModelMapper)
        {
            _rssRepository = rssRepository;
            _mapper = mapper;
            _toServiceModelMapper = toServiceModelMapper;
        }

        public Task AddAsync(string url, CancellationToken cancellationToken = default) =>
            _rssRepository.AddAsync(url, cancellationToken);

        public async Task<RssServiceModel> GetAsync(string id, CancellationToken cancellationToken = default) =>
            _toServiceModelMapper.Transform(await _rssRepository.GetAsync(id, cancellationToken));

        public Task RemoveAsync(string id, CancellationToken token = default) => _rssRepository.RemoveAsync(id, token);

        public async Task<IEnumerable<RssServiceModel>> GetListAsync(CancellationToken token = default) =>
            (await _rssRepository.GetListAsync(token)).Select(w => _toServiceModelMapper.Transform(w));

        public Task UpdateAsync(RssServiceModel rss, CancellationToken token = default) =>
            _rssRepository.UpdateAsync(_mapper.Transform(rss), token);

        public Task LoadAndUpdateAsync(string id, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }

        Task IRssService.UpdatePositionAsync(string localItemId, int position, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task ReadAllMessagesAsync(string itemId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}