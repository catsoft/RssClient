using Shared.App.Base.Database;

namespace Shared.App.Base.Command
{
    /// <typeparam name="T">Response</typeparam>
    /// <typeparam name="TE">Request</typeparam>
    public abstract class BaseCommand<T, TE> : ICommand<T, TE>
    where T : BaseResponse
    {
        public ILocalDb LocalDatabase { get; set; }
        public ICommandDelegate<T> Delegate { get; set; }
        
        protected BaseCommand(ILocalDb localDb, ICommandDelegate<T> commandDelegate)
        {
            LocalDatabase = localDb;
            Delegate = commandDelegate;
        }

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