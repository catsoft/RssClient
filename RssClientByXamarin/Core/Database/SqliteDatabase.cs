using System;
using System.IO;
using Core.Database.Rss;
using SQLite;

namespace Core.Database
{
    public class SqliteDatabase
    {
        [NotNull] public SQLiteConnection Connection { get; }
        
        public SqliteDatabase()
        {
            var sqliteFilename = "RssClientDatabase.db3";
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
            var path = Path.Combine (libraryPath, sqliteFilename);
            
            Connection = new SQLiteConnection(path);

            TryCreateTable<SettingsModel>();
            TryCreateTable<RssModel>();
            TryCreateTable<RssMessageModel>();
        }

        public void TryCreateTable<T>()
        {
            try
            {
                Connection.CreateTable<T>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}