using System;
using System.Linq;
using Realms;

namespace Database
{
	public interface IDatabase : IDisposable
	{
		void Remove<T>(T item) where T : RealmObject;
		void RemoveRange<T>(IQueryable<T> item) where T : RealmObject;
		void RemoveAll<T>() where T : RealmObject;

		void Add<T>(T item) where T : RealmObject;
		void AddOrUpdate<T>(T item) where T : RealmObject;

		T Find<T>(string primaryKey) where T : RealmObject;

		IQueryable<T> All<T>() where T : RealmObject;
	}
}