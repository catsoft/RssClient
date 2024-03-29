﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration.AllMessageFilter;
using Core.Configuration.Settings;
using Core.Database;
using Core.Database.Rss;
using Core.Extensions;
using Core.Infrastructure.Mappers;
using Core.Repositories.Configurations;
using JetBrains.Annotations;
using SQLite;

namespace Core.Repositories.RssMessage
{
    public class RssMessagesRepository : IRssMessagesRepository
    {
        [JetBrains.Annotations.NotNull] private readonly SqliteDatabase _sqliteDatabase;
        [JetBrains.Annotations.NotNull] private readonly IConfigurationRepository _configurationRepository;
        [JetBrains.Annotations.NotNull] private readonly IMapper<RssMessageModel, RssMessageDomainModel> _mapperToDomain;
        [JetBrains.Annotations.NotNull] private readonly IMapper<RssMessageDomainModel, RssMessageModel> _mapperToModel;

        public RssMessagesRepository(
            [JetBrains.Annotations.NotNull] SqliteDatabase sqliteDatabase,
            [JetBrains.Annotations.NotNull] IConfigurationRepository configurationRepository,
            [JetBrains.Annotations.NotNull] IMapper<RssMessageModel, RssMessageDomainModel> mapperToDomain,
            [JetBrains.Annotations.NotNull] IMapper<RssMessageDomainModel, RssMessageModel> mapperToModel)
        {
            _sqliteDatabase = sqliteDatabase;
            _configurationRepository = configurationRepository;
            _mapperToDomain = mapperToDomain;
            _mapperToModel = mapperToModel;
        }

        public Task AddAsync(RssMessageDomainModel messageDomainModel, Guid idRss, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
                {
                    var messageModel = _mapperToModel.Transform(messageDomainModel);
                    messageModel.Id = Guid.NewGuid();
                    messageModel.RssId = idRss;

                    connection.NotNull().Insert(messageModel);
                },
                token);
        }

        public Task<RssMessageDomainModel> GetAsync(Guid id, CancellationToken token)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
                {
                    var item = connection.NotNull().Find<RssMessageModel>(id).NotNull();

                    var mappedItem = _mapperToDomain.Transform(item);
                    
                    mappedItem = FillRssData(mappedItem);

                    return mappedItem;
                },
                token);
        }

        private RssMessageDomainModel FillRssData(RssMessageDomainModel model)
        {
            return _sqliteDatabase.DoWithConnection((connection) =>
            {
                var rss = connection.NotNull().Find<RssFeedModel>(model.RssId).NotNull();

                model.RssTitle = rss.Name;
                model.RssIcon = rss.UrlPreviewImage;

                return model;
            });
        }

        public Task UpdateAsync(RssMessageDomainModel message, CancellationToken token)
        {
            if (message == null) return Task.CompletedTask;

            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
                {
                    var newModel = _mapperToModel.Transform(message);
                    connection.NotNull().Update(newModel);
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetMessagesForRss(Guid rssId, CancellationToken token)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) => GetAllMessagesInner(connection)
                    .Where(w => w.RssId == rssId)
                    .Select(_mapperToDomain.Transform)
                    .ToList()
                    .AsEnumerable(),
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllMessages(CancellationToken token)
        {
            return _sqliteDatabase.DoWithConnectionAsync(
                connection => GetAllMessagesInner(connection)
                    .ToList()
                    .Select(_mapperToDomain.Transform)
                    .Select(FillRssData)
                    .ToList()
                    .AsEnumerable(),
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllFavoriteMessages(CancellationToken token)
        {
            return _sqliteDatabase.DoWithConnectionAsync(
                (connection) => GetAllMessagesInner(connection)
                    .Where(w => w.IsFavorite)
                    .ToList()
                    .Select(_mapperToDomain.Transform)
                    .Select(FillRssData)
                    .ToList()
                    .AsEnumerable(),
                token);
        }

        public Task DeleteRssFeedMessages(Guid id, CancellationToken token = default)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
                {
                    var messages = connection.NotNull().Table<RssMessageModel>().NotNull().Where(w => w.RssId == id).NotNull();
                    foreach (var rssMessageModel in messages) connection.NotNull().Delete(rssMessageModel);
                },
                token);
        }

        public Task<RssMessageDomainModel> GetMessageBySyndicationIdAsync(string syndicationId, Guid rssId, CancellationToken token)
        {
            return _sqliteDatabase.DoWithConnectionAsync((connection) =>
                {
                    var item = connection.NotNull()
                        .Table<RssMessageModel>()
                        ?.FirstOrDefault(w => w.RssId == rssId && w.SyndicationId == syndicationId);

                    return item == null ? null : _mapperToDomain.Transform(item);
                },
                token);
        }

        public Task<IEnumerable<RssMessageDomainModel>> GetAllFilterMessages(AllMessageFilterConfiguration filterConfiguration,
            CancellationToken token)
        {
            return _sqliteDatabase.DoWithConnectionAsync(connection =>
                {
                    var messages = GetAllMessagesInner(connection);

                    messages = filterConfiguration?.ApplyFilter(messages);
                    messages = filterConfiguration?.ApplyDateFilter(messages);
                    messages = messages ?? new List<RssMessageModel>().AsQueryable();

                    return messages.ToList()
                        .Select(_mapperToDomain.Transform)
                        .Select(FillRssData)
                        .ToList()
                        .AsEnumerable();
                },
                token);
        }

        [JetBrains.Annotations.NotNull]
        [ItemNotNull]
        private IEnumerable<RssMessageModel> GetAllMessagesInner(SQLiteConnection connection)
        {
            var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
            var hideReadMessages = appConfiguration.HideReadMessages;
            var messages = connection.NotNull().Table<RssMessageModel>();

            if (hideReadMessages)
                messages = messages?.Where(w => !w.IsRead);

            var filter = _configurationRepository.GetSettings<AllMessageFilterConfiguration>();

            var enumerableMessages = messages?.ToList() ?? new List<RssMessageModel>().AsEnumerable().NotNull();
            enumerableMessages = filter.ApplySort(enumerableMessages);

            return enumerableMessages;
        }
    }
}
