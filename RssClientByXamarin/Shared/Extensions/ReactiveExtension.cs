#region

using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using JetBrains.Annotations;
using ReactiveUI;

#endregion

namespace Shared.Extensions
{
    public static class ReactiveExtension
    {
        public static void AddTo(this IDisposable @this, [CanBeNull] CompositeDisposable disposable) { disposable?.Add(@this); }

        [NotNull]
        public static IObservable<Unit> SelectUnit<T>([NotNull] this IObservable<T> obs) { return obs.Select(x => Unit.Default); }

        [NotNull]
        public static IDisposable ExecuteNow<TParam, TResult>([NotNull] this ReactiveCommand<TParam, TResult> cmd, TParam param = default)
        {
            return cmd.CanExecute.NotNull().Take(1).Where(x => x).Select(_ => param).BindCommand(cmd).NotNull();
        }
    }

    public static class ObservableExtensions
    {
        [NotNull]
        public static IDisposable BindCommand<TIn, TOut>(this IObservable<TIn> @this, [NotNull] ReactiveCommand<TIn, TOut> cmd)
        {
            return @this.InvokeCommand(cmd).NotNull();
        }
    }
}
