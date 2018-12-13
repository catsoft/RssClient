using Realms;

namespace Database
{
	public class RealmDatabase
	{
		private static RealmDatabase _instance;
		public static RealmDatabase Instance => _instance ?? (_instance = new RealmDatabase());

		private const string DatabaseFilename = "librarydb.realm";

		public Realm Realm { get; }

		private RealmDatabase()
		{
			Realm = Realm.GetInstance(DatabaseFilename);
		}

		public void Dispose()
		{
			Realm.Dispose();
		}
	}
}