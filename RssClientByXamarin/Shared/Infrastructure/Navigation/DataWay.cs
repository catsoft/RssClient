using JetBrains.Annotations;
using Shared.Infrastructure.ViewModels;

namespace Shared.Infrastructure.Navigation
{
    public abstract class DataWay<TViewModel, TData> : IWay<TViewModel>
        where TViewModel : ViewModel
    {
        [CanBeNull] public TData Data { get; set; }

        public abstract void Go();
    }
}
