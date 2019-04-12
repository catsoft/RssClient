using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace Shared.Extensions
{
    public static class ReactiveExtension
    {
        public static void AddTo(this IDisposable @this, CompositeDisposable disposable) => disposable.Add(@this);

        public static IObservable<Unit> SelectUnit<T>(this IObservable<T> obs) => obs.Select(x => Unit.Default);
        
        public static IDisposable ExecuteNow<TParam, TResult>(this ReactiveCommand<TParam, TResult> cmd, TParam param = default)
        {
            return cmd.CanExecute.Take(1).Where(x => x).Select(_ => param).BindCommand(cmd);
        }
    }
    
    public static class ObservableExtensions
    {
        public static IDisposable BindCommand<TIn, TOut>(this IObservable<TIn> @this, ReactiveCommand<TIn, TOut> cmd)
        {
            return @this.InvokeCommand(cmd);
        }
    }
}