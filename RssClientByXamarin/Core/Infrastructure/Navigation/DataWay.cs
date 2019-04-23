using Core.Infrastructure.ViewModels;
using JetBrains.Annotations;

namespace Core.Infrastructure.Navigation
{
    public abstract class DataWay<TViewModel, TData> : IWay<TViewModel>
        where TViewModel : ViewModel
    {
        [CanBeNull] public TData Data { get; set; }

        public abstract void Go();
    }
}
