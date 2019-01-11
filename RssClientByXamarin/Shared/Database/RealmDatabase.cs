using System;
using System.Threading.Tasks;
using Database.Rss;
using Realms;

namespace Database
{
	public class RealmDatabase
	{
		private static RealmDatabase _instance;
		public static RealmDatabase Instance => _instance ?? (_instance = new RealmDatabase());

		private const string DatabaseFilename = "librarydb.realm";

		public Realm MainThreadRealm { get; }

		private RealmDatabase()
		{
            var config = new RealmConfiguration(DatabaseFilename);
            try
            {
                MainThreadRealm = Realm.GetInstance(config);
            }
            catch (Realms.Exceptions.RealmMigrationNeededException e)
            {
                Realm.DeleteRealm(config);
                MainThreadRealm = Realm.GetInstance(config);
            }
        }

        public Realm OpenDatabase => Realm.GetInstance(DatabaseFilename);

        public void Dispose()
		{
			MainThreadRealm.Dispose();
		}

        public Task DoInBackground(Action<Realm> action)
        {
            return Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite())
                    {
                        action?.Invoke(realm);

                        transaction.Commit();
                    }
                }
            });
        }

        public Task UpdateInBackground<TModel>(string id, Action<TModel> action)
        where TModel : RealmObject, IHaveId
        {
            return DoInBackground(realm =>
            {
                var currentItem = realm.Find<TModel>(id);

                action?.Invoke(currentItem);
            });
        }

        public async Task<string> InsertInBackground<TModel>(TModel model)
            where TModel : RealmObject, IHaveId
        {
            return await Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite())
                    {
                        var item = realm.Add(model);

                        transaction.Commit();

                        return item.Id;
                    }
                }
            });
        }
    }
}