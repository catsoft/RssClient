using Shared.Infrastructure.ViewModels;

namespace Shared.Infrastructure.Navigation
{
    public abstract class DataWay<TViewModel, TData> : IWay<TViewModel>
        where TViewModel : ViewModel
    {
        public abstract void Go();
        public TData Data { get; set; }
    }
}