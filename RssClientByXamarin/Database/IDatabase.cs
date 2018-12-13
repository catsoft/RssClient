using System;
using System.Linq;

namespace Database
{
	public interface IDatabase : IDisposable
	{
		void Remove<T>(T item) where T : Entity;
		void RemoveRange<T>(IQueryable<T> item) where T : Entity;
		void RemoveAll<T>() where T : Entity;

		void Add<T>(T item) where T : Entity;
		void AddOrUpdate<T>(T item) where T : Entity;

		T Find<T>(string primaryKey) where T : Entity;

		IQueryable<T> All<T>() where T : Entity;
	}
}