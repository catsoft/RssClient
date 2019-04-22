using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Realms;
using Realms.Exceptions;
using Shared.Extensions;

namespace Shared.Database
{
    public class RealmDatabase
    {
        private const string DatabaseFilename = "librarydb.realm";

        public RealmDatabase()
        {
            var config = new RealmConfiguration(DatabaseFilename);
            try
            {
                MainThreadRealm = Realm.GetInstance(config).NotNull();
            }
            catch (RealmMigrationNeededException)
            {
                Realm.DeleteRealm(config);
                MainThreadRealm = Realm.GetInstance(config).NotNull();
            }
        }

        [NotNull] public Realm MainThreadRealm { get; }

        [NotNull] public static Realm OpenDatabase => Realm.GetInstance(DatabaseFilename).NotNull();

        public void Dispose() { MainThreadRealm.Dispose(); }

        [NotNull]
        public static Task DoInBackground([CanBeNull] Action<Realm> action)
        {
            return Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite().NotNull())
                    {
                        action?.Invoke(realm);

                        transaction.Commit();
                    }
                }
            });
        }

        [NotNull]
        [ItemCanBeNull]
        public static Task<TModel> GetAsync<TModel>([CanBeNull] string id)
            where TModel : RealmObject, IHaveId
        {
            return Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    return realm.Find<TModel>(id);
                }
            });
        }

        [NotNull]
        public static Task UpdateAsync<TModel>([CanBeNull] string id, [CanBeNull] Action<TModel, Realm> action)
            where TModel : RealmObject, IHaveId
        {
            return DoInBackground(realm =>
            {
                var currentItem = realm.NotNull().Find<TModel>(id);

                action?.Invoke(currentItem, realm);
            });
        }

        [NotNull]
        [ItemCanBeNull]
        public static async Task<string> InsertAsync<TModel>([CanBeNull] TModel model)
            where TModel : RealmObject, IHaveId
        {
            return await Task.Run(() =>
            {
                using (var realm = OpenDatabase)
                {
                    using (var transaction = realm.BeginWrite().NotNull())
                    {
                        var item = realm.Add(model).NotNull();

                        transaction.Commit();

                        return item.Id;
                    }
                }
            });
        }
    }
}
