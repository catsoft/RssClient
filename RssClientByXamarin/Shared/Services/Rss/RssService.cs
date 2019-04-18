using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Shared.Api.Rss;
using Shared.Database;
using Shared.Database.Rss;
using Shared.Infrastructure.Mappers;
using Shared.Repository.Rss;
using Shared.Repository.RssMessage;
using Shared.Utils;

namespace Shared.Services.Rss
{
    public class RssService : IRssService
    {
        private readonly IRssRepository _rssRepository;
        private readonly IMapper<RssServiceModel, RssDomainModel> _mapper;
        private readonly IMapper<RssDomainModel, RssServiceModel> _toServiceModelMapper;
        private readonly IRssApiClient _rssApiClient;
        private readonly IRssMessagesRepository _rssMessagesRepository;

        public RssService(IRssRepository rssRepository, IMapper<RssServiceModel, RssDomainModel> mapper,
            IMapper<RssDomainModel, RssServiceModel> toServiceModelMapper, IRssApiClient rssApiClient, IRssMessagesRepository rssMessagesRepository)
        {
            _rssRepository = rssRepository;
            _mapper = mapper;
            _toServiceModelMapper = toServiceModelMapper;
            _rssApiClient = rssApiClient;
            _rssMessagesRepository = rssMessagesRepository;
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

        public async Task LoadAndUpdateAsync(string id, CancellationToken token = default)
        {
            var currentItem = await _rssRepository.GetAsync(id, token);
            var syndicationFeed = await _rssApiClient.LoadFeedsAsync(currentItem.Rss, token);

            if (syndicationFeed == null)
                return;

            currentItem = await _rssRepository.GetAsync(id, token);
            currentItem.Name = syndicationFeed.Title?.Text;
            currentItem.UpdateTime = DateTime.Now;
            //TODO сюда запихнуть фавикон
            currentItem.UrlPreviewImage = syndicationFeed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";
            await _rssRepository.UpdateAsync(currentItem, token);

            foreach (var syndicationItem in syndicationFeed.Items.Where(w => w != null))
            {
                var imageUri = syndicationItem.Links.FirstOrDefault(w =>
                        w.RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) ==
                        true && w.MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)
                    ?.Uri?.OriginalString;

                var url = syndicationItem.Links.FirstOrDefault(w =>
                        w.RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)
                    ?.Uri
                    ?.OriginalString;

                var item = new RssMessageDomainModel()
                {
                    SyndicationId = syndicationItem.Id,
                    Title = syndicationItem.Title?.Text?.SafeTrim(),
                    Text = syndicationItem?.Summary?.Text?.SafeTrim(),
                    CreationDate = syndicationItem.PublishDate.Date,
                    Url = url,
                    ImageUrl = imageUri,
                };
                
                await _rssMessagesRepository.AddMessageAsync(item, id, token);
            }
        }

        public async Task UpdatePositionAsync(string localItemId, int position, CancellationToken token)
        {
            var item = await _rssRepository.GetAsync(localItemId, token);
            item.Position = position;
            await _rssRepository.UpdateAsync(item, token);
        }

        public Task ReadAllMessagesAsync(string itemId, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}