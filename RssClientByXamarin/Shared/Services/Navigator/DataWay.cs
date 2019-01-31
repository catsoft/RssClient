using Shared.ViewModels;

namespace Shared.Services.Navigator
{
    public abstract class DataWay<TViewModel, TData> : IWay<TViewModel>
        where TViewModel : ViewModel
    {
        public abstract void Go();
        public TData Data { get; set; }
    }
}