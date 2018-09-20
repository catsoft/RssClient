using System;

namespace Shared.App.Base.Command
{
    public class CommandDelegate<T> : ICommandDelegate<T>
    {
        public Action<Error> OnFailed { get; set; }
        public Action OnNotConnection { get; set; }
        public Action<T> OnSuccess { get; set; }

        public CommandDelegate(Action<T> onSuccess = null, Action<Error> onFailed = null, Action onNotConnection = null)
        {
            OnFailed = onFailed;
            OnNotConnection = onNotConnection;
            OnSuccess = onSuccess;
        }
    }
}
