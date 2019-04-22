using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Shared.Api.Rss;
using Shared.Extensions;
using Shared.Infrastructure.Mappers;
using Shared.Repositories.Rss;
using Shared.Repositories.RssMessage;
using Shared.Utils;

namespace Shared.Services.Rss
{
    public class RssService : IRssService
    {
        [NotNull] private readonly IMapper<RssServiceModel, RssDomainModel> _mapper;
        [NotNull] private readonly IRssApiClient _rssApiClient;
        [NotNull] private readonly IRssMessagesRepository _rssMessagesRepository;
        [NotNull] private readonly IRssRepository _rssRepository;
        [NotNull] private readonly IMapper<RssDomainModel, RssServiceModel> _toServiceModelMapper;

        public RssService(
            [NotNull] IRssRepository rssRepository,
            [NotNull] IMapper<RssServiceModel, RssDomainModel> mapper,
            [NotNull] IMapper<RssDomainModel, RssServiceModel> toServiceModelMapper,
            [NotNull] IRssApiClient rssApiClient,
            [NotNull] IRssMessagesRepository rssMessagesRepository)
        {
            _rssRepository = rssRepository;
            _mapper = mapper;
            _toServiceModelMapper = toServiceModelMapper;
            _rssApiClient = rssApiClient;
            _rssMessagesRepository = rssMessagesRepository;
        }

        public Task AddAsync(string url, CancellationToken cancellationToken = default) { return _rssRepository.AddAsync(url, cancellationToken); }

        public async Task<RssServiceModel> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            return _toServiceModelMapper.Transform(await _rssRepository.GetAsync(id, cancellationToken));
        }

        public Task RemoveAsync(string id, CancellationToken token = default) { return _rssRepository.RemoveAsync(id, token); }

        public async Task<IEnumerable<RssServiceModel>> GetListAsync(CancellationToken token = default)
        {
            return (await _rssRepository.GetListAsync(token)).Select(w => _toServiceModelMapper.Transform(w));
        }

        public Task UpdateAsync(RssServiceModel rss, CancellationToken token = default)
        {
            return _rssRepository.UpdateAsync(_mapper.Transform(rss), token);
        }

        public async Task LoadAndUpdateAsync(string id, CancellationToken token = default)
        {
            var currentItem = await _rssRepository.GetAsync(id, token);
            var syndicationFeed = await _rssApiClient.LoadFeedsAsync(currentItem?.Rss, token);

            if (syndicationFeed == null) return;

            currentItem = await _rssRepository.GetAsync(id, token);
            
            if (currentItem == null) return;
            
            currentItem.Name = syndicationFeed.Title?.Text;
            currentItem.UpdateTime = DateTime.Now;
            //TODO сюда запихнуть фавикон
            currentItem.UrlPreviewImage = syndicationFeed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";
            await _rssRepository.UpdateAsync(currentItem, token);

            foreach (var syndicationItem in syndicationFeed.Items?.Where(w => w != null) ?? new SyndicationItem[0])
            {
                var notNulLinks = syndicationFeed.Links?.Where(w => w != null).ToList() ?? new List<SyndicationLink>();
                var imageUri = notNulLinks.FirstOrDefault(w =>
                        w.NotNull().RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) == true
                        && w.NotNull().MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)
                    ?.Uri?.OriginalString;

                var url = notNulLinks
                    .FirstOrDefault(w => w.NotNull().RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)
                    ?.Uri?.OriginalString;

                var item = new RssMessageDomainModel
                {
                    SyndicationId = syndicationItem.Id,
                    Title = syndicationItem.Title?.Text?.SafeTrim(),
                    Text = syndicationItem.Summary?.Text?.SafeTrim(),
                    CreationDate = syndicationItem.PublishDate.Date,
                    Url = url,
                    ImageUrl = imageUri
                };

                await _rssMessagesRepository.AddMessageAsync(item, id, token);
            }
        }

        public async Task UpdatePositionAsync(string localItemId, int position, CancellationToken token)
        {
            var item = await _rssRepository.GetAsync(localItemId, token);
            if (item != null)
            {
                item.Position = position;
                await _rssRepository.UpdateAsync(item, token);
            }
        }

        public Task ReadAllMessagesAsync(string itemId, CancellationToken token = default) { throw new NotImplementedException(); }
        
        public async Task ShareAsync(RssServiceModel model, CancellationToken token = default)
        {
            await Xamarin.Essentials.Share.RequestAsync(model?.Rss).NotNull();
        }
    }
}
