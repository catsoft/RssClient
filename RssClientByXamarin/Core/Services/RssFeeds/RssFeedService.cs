using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Core.Api.RssFeeds;
using Core.Extensions;
using Core.Infrastructure.Mappers;
using Core.Repositories.RssFeeds;
using Core.Repositories.RssMessage;
using Core.Utils;
using JetBrains.Annotations;

namespace Core.Services.RssFeeds
{
    public class RssFeedService : IRssFeedService
    {
        [NotNull] private readonly IMapper<RssFeedServiceModel, RssFeedDomainModel> _mapper;
        [NotNull] private readonly IRssFeedApiClient _rssFeedApiClient;
        [NotNull] private readonly IRssMessagesRepository _rssMessagesRepository;
        [NotNull] private readonly IRssFeedRepository _rssFeedRepository;
        [NotNull] private readonly IMapper<RssFeedDomainModel, RssFeedServiceModel> _toServiceModelMapper;

        public RssFeedService(
            [NotNull] IRssFeedRepository rssFeedRepository,
            [NotNull] IMapper<RssFeedServiceModel, RssFeedDomainModel> mapper,
            [NotNull] IMapper<RssFeedDomainModel, RssFeedServiceModel> toServiceModelMapper,
            [NotNull] IRssFeedApiClient rssFeedApiClient,
            [NotNull] IRssMessagesRepository rssMessagesRepository)
        {
            _rssFeedRepository = rssFeedRepository;
            _mapper = mapper;
            _toServiceModelMapper = toServiceModelMapper;
            _rssFeedApiClient = rssFeedApiClient;
            _rssMessagesRepository = rssMessagesRepository;
        }

        public Task AddAsync(string url, CancellationToken cancellationToken = default) { return _rssFeedRepository.AddAsync(url, cancellationToken); }

        public async Task<RssFeedServiceModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _toServiceModelMapper.Transform(await _rssFeedRepository.GetAsync(id, cancellationToken));
        }

        public async Task RemoveAsync(Guid id, CancellationToken token = default)
        {
            await _rssMessagesRepository.DeleteRssFeedMessages(id, token);
            
            await _rssFeedRepository.RemoveAsync(id, token);
        }

        public async Task<IEnumerable<RssFeedServiceModel>> GetListAsync(CancellationToken token = default)
        {
            return (await _rssFeedRepository.GetListAsync(token)).Select(w => _toServiceModelMapper.Transform(w));
        }

        public Task UpdateAsync(RssFeedServiceModel rssFeed, CancellationToken token = default)
        {
            return _rssFeedRepository.UpdateAsync(_mapper.Transform(rssFeed), token);
        }

        public async Task LoadAndUpdateAsync(Guid id, CancellationToken token = default)
        {
            var currentItem = await _rssFeedRepository.GetAsync(id, token);
            var syndicationFeed = await _rssFeedApiClient.LoadFeedsAsync(currentItem?.Rss, token);

            if (syndicationFeed == null) return;

            currentItem = await _rssFeedRepository.GetAsync(id, token);
            
            if (currentItem == null) return;
            
            currentItem.Name = syndicationFeed.Title?.Text;
            currentItem.UpdateTime = DateTime.Now;
            //TODO сюда запихнуть фавикон
            currentItem.UrlPreviewImage = syndicationFeed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";
            await _rssFeedRepository.UpdateAsync(currentItem, token);

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

        public async Task UpdatePositionAsync(Guid localItemId, int position, CancellationToken token)
        {
            var item = await _rssFeedRepository.GetAsync(localItemId, token);
            if (item != null)
            {
                item.Position = position;
                await _rssFeedRepository.UpdateAsync(item, token);
            }
        }

        public Task ReadAllMessagesAsync(Guid itemId, CancellationToken token = default) { throw new NotImplementedException(); }
        
        public async Task ShareAsync(RssFeedServiceModel model, CancellationToken token = default)
        {
            await Xamarin.Essentials.Share.RequestAsync(model?.Rss).NotNull();
        }
    }
}
