#region

using Shared.Infrastructure.ViewModels;

#endregion

namespace Shared.Infrastructure.Navigation
{
    public abstract class DataWay<TViewModel, TData> : IWay<TViewModel>
        where TViewModel : ViewModel
    {
        public TData Data { get; set; }
        public abstract void Go();
    }
}
