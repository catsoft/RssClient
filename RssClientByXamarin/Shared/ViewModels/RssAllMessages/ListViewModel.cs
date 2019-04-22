using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Shared.Extensions;
using ViewModel = Shared.Infrastructure.ViewModels.ViewModel;

namespace Shared.ViewModels.RssAllMessages
{
    public class ListViewModel<T> : ViewModel
        where T : class
    {
        public ListViewModel([NotNull] ReactiveCommand<Unit, IEnumerable<T>> loadCommand)
        {
            SourceList = new SourceList<T>();
            SourceList.CountChanged.NotNull().Select(w => w == 0).ToPropertyEx(this, model => model.IsEmpty);

            loadCommand.Subscribe(w =>
            {
                SourceList.Clear();
                SourceList.AddRange(w);
            });
        }
        
        [NotNull] public SourceList<T> SourceList { get; }
        
        [NotNull] public IObservable<IChangeSet<T>> ConnectChanges => SourceList.Connect().NotNull();
        
        public extern bool IsEmpty { [ObservableAsProperty] get; }
    }
}