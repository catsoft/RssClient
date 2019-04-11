using Shared.Infrastructure.ViewModels;

namespace Shared.Infrastructure.Navigation
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