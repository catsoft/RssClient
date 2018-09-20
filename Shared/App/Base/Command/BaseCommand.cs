using Shared.App.Base.Database;

namespace Shared.App.Base.Command
{
    /// <typeparam name="T">Передаваемый тип в onsuccess</typeparam>
    /// <typeparam name="TE">Тип модели для выполнения</typeparam>
    public abstract class BaseCommand<T, TE> : ICommand<T, TE>
    where T : BaseResponse
    {
        public ILocalDb LocalDatabase { get; set; }
        public ICommandDelegate<T> Delegate { get; set; }

#if __ANDROID__
        public Android.Content.Context Context { get; set; }

        protected BaseCommand(Android.Content.Context context, ILocalDb localDb, ICommandDelegate<T> commandDelegate)
        {
            Context = context;
            LocalDatabase = localDb;
            Delegate = commandDelegate;
        }
#endif

#if __IOS__
        protected BaseCommand(ILocalDb localDb, ICommandDelegate<T> commandDelegate)
        {
            LocalDatabase = localDb;
            Delegate = commandDelegate;
        }
#endif


        public abstract void Execute(TE model);

        public void CommonExecute(T model)
        {
            if (model == null)
            {
                Delegate?.OnNotConnection?.Invoke();
            }
            else
            {
                if (model.IsSuccess)
                {
                    Delegate?.OnSuccess?.Invoke(model);
                }
                else
                {
                    Delegate?.OnFailed?.Invoke(model.Error);
                }
            }
        }
    }
}