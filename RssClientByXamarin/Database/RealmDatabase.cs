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
			MainThreadRealm = Realm.GetInstance(DatabaseFilename);
		}

        public Realm OpenDatabase => Realm.GetInstance(DatabaseFilename);

        public void Dispose()
		{
			MainThreadRealm.Dispose();
		}

        public async void DoInBackground<TModel>(TModel model, Action<TModel> action)
        where TModel : RealmObject, IHaveId
        {
            var id = model.Id;

            await Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    var currentItem = realm.Find<TModel>(id);

                    using (var transaction = realm.BeginWrite())
                    {
                        action?.Invoke(currentItem);

                        transaction.Commit();
                    }
                }
            });
        }
	}
}