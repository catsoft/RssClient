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
	}
}