using System;

namespace Shared.App.Base.Command
{
    public interface ICommandDelegate<T>
    {
        Action<Error> OnFailed { get; set; }
        Action OnNotConnection { get; set; }
        Action<T> OnSuccess { get; set; }
    }
}
