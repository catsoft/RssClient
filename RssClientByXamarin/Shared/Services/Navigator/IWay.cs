using Shared.ViewModels;

namespace Shared.Services.Navigator
{
    public interface IWay<TViewModel> : IWay
    where TViewModel : ViewModel
    {
    }
    
    public interface IWay
    {
        void Go();
    }
}