using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Database.Rss;
using SQLite;

namespace Core.Database
{
    public class SqliteDatabase
    {
        [JetBrains.Annotations.NotNull] private readonly string _dbPath;
        
        public SqliteDatabase()
        {
            var sqliteFilename = "RssClientDatabase.db3";
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
            _dbPath = Path.Combine (libraryPath, sqliteFilename);
            
            var connection = new SQLiteConnection(_dbPath);

            TryCreateTable<SettingsModel>(connection);
            TryCreateTable<RssFeedModel>(connection);
            TryCreateTable<RssMessageModel>(connection);
            
            connection.Commit();
            connection .Close();
        }

        [JetBrains.Annotations.NotNull] public SQLiteConnection OpenConnection => new SQLiteConnection(_dbPath);
        
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
            using (var connection = OpenConnection)
            {
                action?.Invoke(connection);
                connection.Commit();
            }
        }
        
        public T DoWithConnection<T>(Func<SQLiteConnection, T> action)
        {
            using (var connection = OpenConnection)
            {
                var t = action.Invoke(connection);
                connection.Commit();

                return t;
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