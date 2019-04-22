using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Configuration.Settings;
using Shared.Extensions;
using Shared.Infrastructure.Mappers;
using Shared.Repositories.RssMessage;

namespace Shared.Database.Rss
{
    public class RssMessageService : IRssMessageService
    {
        [NotNull] private readonly IRssMessagesRepository _rssMessagesRepository;
        [NotNull] private readonly IMapper<RssMessageServiceModel, RssMessageDomainModel> _toDomainMapper;
        [NotNull] private readonly IMapper<RssMessageDomainModel, RssMessageServiceModel> _toServiceMapper;

        public RssMessageService([NotNull] IRssMessagesRepository rssMessagesRepository,
            [NotNull] IMapper<RssMessageServiceModel, RssMessageDomainModel> toDomainMapper,
            [NotNull] IMapper<RssMessageDomainModel, RssMessageServiceModel> toServiceMapper)
        {
            _rssMessagesRepository = rssMessagesRepository;
            _toDomainMapper = toDomainMapper;
            _toServiceMapper = toServiceMapper;
        }

        public Task AddMessageAsync(RssMessageServiceModel message, string idRss, CancellationToken token = default)
        {
            return _rssMessagesRepository.AddMessageAsync(_toDomainMapper.Transform(message), idRss, token);
        }

        public async Task<RssMessageServiceModel> GetAsync(string id, CancellationToken token = default)
        {
            return _toServiceMapper.Transform(await _rssMessagesRepository.GetAsync(id, token));
        }

        public Task UpdateAsync(RssMessageServiceModel message, CancellationToken token = default)
        {
            return _rssMessagesRepository.UpdateAsync(_toDomainMapper.Transform(message), token);
        }

        public async Task<IEnumerable<RssMessageServiceModel>> GetMessagesForRss(string rssId, CancellationToken token = default)
        {
            return (await _rssMessagesRepository.GetMessagesForRss(rssId, token)).Select(w => _toServiceMapper.Transform(w));
        }

        public async Task<IEnumerable<RssMessageServiceModel>> GetAllMessages(CancellationToken token = default)
        {
            return (await _rssMessagesRepository.GetAllMessages(token)).Select(w => _toServiceMapper.Transform(w));
        }

        public async Task<IEnumerable<RssMessageServiceModel>> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration,
            CancellationToken token = default)
        {
            return (await _rssMessagesRepository.GetAllFilterMessages(filterConfiguration, token)).Select(w => _toServiceMapper.Transform(w));
        }

        public async Task<IEnumerable<RssMessageServiceModel>> GetAllFavoriteMessages(CancellationToken token = default)
        {
            return (await _rssMessagesRepository.GetAllFavoriteMessages(token)).Select(w => _toServiceMapper.Transform(w));
        }
        
        public async Task ShareAsync(RssMessageServiceModel model, CancellationToken token = default)
        {
            await Xamarin.Essentials.Share.RequestAsync(model?.Url).NotNull();
        }
    }
}
