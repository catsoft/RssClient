using System.Collections.Generic;
using System.Linq;
using Realms;
using Remotion.Linq.Clauses;

namespace Database
{
	public class RealmDatabase : IDatabase
	{
		private static RealmDatabase _instance;
		public static RealmDatabase Instance => _instance ?? (_instance = new RealmDatabase());

		private const string DatabaseFilename = "database1.db3";

		private readonly Realm _connection;

		private RealmDatabase()
		{
			_connection = Realm.GetInstance(DatabaseFilename);
		}

		public void Dispose()
		{
			_connection.Dispose();
		}

		public void Remove<T>(T item) where T : Entity
		{
			_connection.Remove(item);
		}

		public void RemoveRange<T>(IQueryable<T> items) where T : Entity
		{
			_connection.RemoveRange(items);
		}

		public void RemoveAll<T>() where T : Entity
		{
			_connection.RemoveAll<T>();
		}

		public void Add<T>(T item) where T : Entity
		{
			_connection.Add(item);
		}

		public void AddOrUpdate<T>(T item) where T : Entity
		{
			_connection.Add(item, true);
		}

		public T Find<T>(string primaryKey) where T : Entity
		{
			return _connection.Find<T>(primaryKey);
		}

		public IQueryable<T> All<T>() where T : Entity
		{
			return _connection.All<T>();
		}
	}
}
