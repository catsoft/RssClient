using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using Core.Analytics;
using Core.Api.RssFeeds;
using Core.Extensions;
using Core.Infrastructure.Mappers;
using Core.Repositories.RssFeeds;
using Core.Repositories.RssMessage;
using JetBrains.Annotations;

namespace Core.Services.RssFeeds
{
    public class RssFeedService : IRssFeedService
    {
        public event EventHandler CollectionChanged = delegate { };
        
        [NotNull] private readonly IMapper<RssFeedServiceModel, RssFeedDomainModel> _mapper;
        [NotNull] private readonly IRssFeedApiClient _rssFeedApiClient;
        [NotNull] private readonly IRssMessagesRepository _rssMessagesRepository;
        [NotNull] private readonly ILog _log;
        [NotNull] private readonly IRssFeedRepository _rssFeedRepository;
        [NotNull] private readonly IMapper<RssFeedDomainModel, RssFeedServiceModel> _toServiceModelMapper;

        public RssFeedService(
            [NotNull] IRssFeedRepository rssFeedRepository,
            [NotNull] IMapper<RssFeedServiceModel, RssFeedDomainModel> mapper,
            [NotNull] IMapper<RssFeedDomainModel, RssFeedServiceModel> toServiceModelMapper,
            [NotNull] IRssFeedApiClient rssFeedApiClient,
            [NotNull] IRssMessagesRepository rssMessagesRepository,
            [NotNull] ILog log)
        {
            _rssFeedRepository = rssFeedRepository;
            _mapper = mapper;
            _toServiceModelMapper = toServiceModelMapper;
            _rssFeedApiClient = rssFeedApiClient;
            _rssMessagesRepository = rssMessagesRepository;
            _log = log;
        }

        public async Task<Guid> AddAsync(string url, CancellationToken cancellationToken = default)
        {
            var guid = await _rssFeedRepository.AddAsync(url, cancellationToken);
            
            CollectionChanged.Invoke(this, EventArgs.Empty);

            return guid;
        }

        public async Task<RssFeedServiceModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _toServiceModelMapper.Transform(await _rssFeedRepository.GetAsync(id, cancellationToken));
        }

        public async Task RemoveAsync(Guid id, CancellationToken token = default)
        {
            await _rssMessagesRepository.DeleteRssFeedMessages(id, token);

            await _rssFeedRepository.RemoveAsync(id, token);
            
            CollectionChanged.Invoke(this, EventArgs.Empty);
        }

        public async Task<IEnumerable<RssFeedServiceModel>> GetListAsync(CancellationToken token = default)
        {
            return (await _rssFeedRepository.GetListAsync(token)).Select(w => _toServiceModelMapper.Transform(w));
        }

        public async Task UpdateAsync(RssFeedServiceModel rssFeed, CancellationToken token = default)
        {
            await _rssFeedRepository.UpdateAsync(_mapper.Transform(rssFeed), token);
            
            CollectionChanged.Invoke(this, EventArgs.Empty);
        }

        public Task LoadAndUpdateAsync(Guid id, CancellationToken token = default)
        {
            return Task.Run(async () =>
                {
                    var currentItem = await _rssFeedRepository.GetAsync(id, token);
                    var syndicationFeed = await _rssFeedApiClient.LoadFeedsAsync(currentItem?.Rss, token);

                    if (syndicationFeed == null) return;

                    currentItem = await _rssFeedRepository.GetAsync(id, token);

                    if (currentItem == null) return;

                    currentItem.Name = syndicationFeed.Title?.Text;
                    currentItem.UpdateTime = DateTime.Now;
                    if (!currentItem.IsFeedly)
                        currentItem.UrlPreviewImage = syndicationFeed.Links?.FirstOrDefault()?.Uri?.OriginalString + "/favicon.ico";

                    await _rssFeedRepository.UpdateAsync(currentItem, token);

                    foreach (var syndicationItem in syndicationFeed.Items?.Where(w => w != null) ?? new SyndicationItem[0])
                    {
                        var existItem = await _rssMessagesRepository.GetMessageBySyndicationIdAsync(syndicationItem.Id, id, token);

                        if (existItem == null)
                        {
                            var item = new RssMessageDomainModel();

                            item = FillMessage(item, syndicationItem);

                            await _rssMessagesRepository.AddAsync(item, id, token);
                        }
                        else
                        {
                            var item = FillMessage(existItem, syndicationItem);

                            await _rssMessagesRepository.UpdateAsync(item, token);
                        }
                    }
                    
                    CollectionChanged.Invoke(this, EventArgs.Empty);
                },
                token);
        }

        private RssMessageDomainModel FillMessage([NotNull] RssMessageDomainModel model, [NotNull] SyndicationItem syndicationItem)
        {
            var notNulLinks = syndicationItem.Links?.Where(w => w != null).ToList() ?? new List<SyndicationLink>();
            var imageUri = notNulLinks.FirstOrDefault(w =>
                    w.NotNull().RelationshipType?.Equals("enclosure", StringComparison.InvariantCultureIgnoreCase) == true
                    && w.NotNull().MediaType?.Equals("image/jpeg", StringComparison.InvariantCultureIgnoreCase) == true)
                ?.Uri?.OriginalString;

            var url = notNulLinks
                .FirstOrDefault(w => w.NotNull().RelationshipType?.Equals("alternate", StringComparison.InvariantCultureIgnoreCase) == true)
                ?.Uri?.OriginalString;

            var title = syndicationItem.Title?.Text?.SafeTrim();
            var text = syndicationItem.Summary?.Text?.SafeTrim();
            var createDate = syndicationItem.PublishDate.Date;

            model.SyndicationId = syndicationItem.Id;
            model.Title = title;
            model.Text = text;
            model.Url = url;
            model.ImageUrl = imageUri;
            try
            {
                model.CreationDate = createDate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _log.TrackError(e, null);
            }
       
            return model;
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

        public Task ReadAllMessagesAsync(Guid itemId, CancellationToken token = default)
        {
            return Task.Run(async () =>
                {
                    var messages = await _rssMessagesRepository.GetMessagesForRss(itemId, token);
                    foreach (var message in messages.Where(w => !w.NotNull().IsRead))
                    {
                        message.IsRead = true;
                        await _rssMessagesRepository.UpdateAsync(message, token);
                    }
                },
                token);
        }

        public async Task ShareAsync(RssFeedServiceModel model, CancellationToken token = default)
        {
            await Xamarin.Essentials.Share.RequestAsync(model?.Rss).NotNull();
        }
    }
}
