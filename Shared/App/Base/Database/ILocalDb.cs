using System;
using System.Collections.Generic;
namespace Shared.App.Base.Database
{
    public interface ILocalDb : IDisposable
    {
        void DeleteItemByLocalId<T>(int id) where T : IEntity, new();
        void DeleteItemByLocalId<T>(T item) where T : IEntity, new();
        void DeleteItemsByLocalId<T>(IEnumerable<T> items) where T : IEntity, new();
        void DeleteItemsByLocalId<T>(IEnumerable<int> itemIds) where T : IEntity, new();
        void DeleteAll<T>() where T : IEntity, new();
        int AddNewItem<T>(T item) where T : IEntity, new();
        void AddNewItems<T>(IEnumerable<T> items) where T : IEntity, new();
        void UpdateItemByLocalId<T>(T item) where T : IEntity, new();
        void UpdateItemsByLocalId<T>(IEnumerable<T> items) where T : IEntity, new();
        T GetItemByLocalId<T>(int id) where T : class, IEntity, new();
        T GetFirstItem<T>() where T : class, IEntity, new();
        IEnumerable<T> GetItems<T>() where T : IEntity, new();
    }
}