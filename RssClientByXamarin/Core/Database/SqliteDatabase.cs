using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Analytics;
using Core.Database.Rss;
using SQLite;

namespace Core.Database
{
    public class SqliteDatabase
    {
        private readonly ILog _logger;
        private readonly object _locker = new object();
        [JetBrains.Annotations.NotNull] private readonly SQLiteConnection _connection;

        public SqliteDatabase(ILog logger)
        {
            _logger = logger;
            var sqliteFilename = "RssClientDatabase.db3";
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine (libraryPath, sqliteFilename);
            
            _connection = new SQLiteConnection(dbPath);

            TryCreateTable<SettingsModel>(_connection);
            TryCreateTable<RssFeedModel>(_connection);
            TryCreateTable<RssMessageModel>(_connection );
        }

        private void TryCreateTable<T>(SQLiteConnection connection)
        {
            try
            {
                connection.CreateTable<T>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DoWithConnection(Action<SQLiteConnection> action)
        {
            try
            {
                lock (_locker)
                {
                    action?.Invoke(_connection);
                }
            }
            catch (Exception e)
            {
                _logger.TrackError(e, null);
            }
        }
        
        public T DoWithConnection<T>(Func<SQLiteConnection, T> action)
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
        
        public Task DoWithConnectionAsync(Action<SQLiteConnection> action, CancellationToken token = default)
        {
            return Task.Run(() => { DoWithConnection(action); }, token);
        }
        
        public Task<T> DoWithConnectionAsync<T>(Func<SQLiteConnection, T> action, CancellationToken token = default)
        {
            return Task.Run(() => DoWithConnection(action), token);
        }
    }
}