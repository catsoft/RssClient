using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Analytics;
using Core.Database.Rss;
using JetBrains.Annotations;
using SQLite;

namespace Core.Database
{
    public class SqliteDatabase
    {
        [JetBrains.Annotations.NotNull] private readonly ILog _logger;
        [JetBrains.Annotations.NotNull] private readonly object _locker = new object();
        [JetBrains.Annotations.NotNull] private readonly SQLiteConnection _connection;

        public SqliteDatabase([JetBrains.Annotations.NotNull] ILog logger)
        {
            _logger = logger;
            var sqliteFilename = "RssClientDatabase.db3";
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(libraryPath, sqliteFilename);
            
            _connection = new SQLiteConnection(dbPath);

            TryCreateTable<SettingsModel>();
            TryCreateTable<RssFeedModel>();
            TryCreateTable<RssMessageModel>();
        }

        private void TryCreateTable<T>()
        {
            try
            {
                _connection.CreateTable<T>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DoWithConnection([JetBrains.Annotations.NotNull] Action<SQLiteConnection> action)
        {
            try
            {
                lock (_locker)
                {
                    action.Invoke(_connection);
                }
            }
            catch (Exception e)
            {
                _logger.TrackError(e, null);
            }
        }
        
        public T DoWithConnection<T>([JetBrains.Annotations.NotNull] Func<SQLiteConnection, T> action)
        {
            try
            {
                lock (_locker)
                {                
                    return action.Invoke(_connection);
                }
            }
            catch (Exception e)
            {
                _logger.TrackError(e, null);
                return default;
            }
        }
        
        [JetBrains.Annotations.NotNull]
        public Task DoWithConnectionAsync([JetBrains.Annotations.NotNull] Action<SQLiteConnection> action, CancellationToken token = default)
        {
            return Task.Run(() => DoWithConnection(action), token);
        }
        
        [JetBrains.Annotations.NotNull]
        [ItemCanBeNull]
        public Task<T> DoWithConnectionAsync<T>([JetBrains.Annotations.NotNull] Func<SQLiteConnection, T> action, CancellationToken token = default)
        {
            return Task.Run(() => DoWithConnection(action), token);
        }
    }
}