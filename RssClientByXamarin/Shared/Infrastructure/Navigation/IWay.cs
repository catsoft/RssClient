using Shared.Infrastructure.ViewModels;

namespace Shared.Infrastructure.Navigation
{
    public interface IWayWithParameters<TViewModel, TParameter> : IWay<TViewModel>
        where TViewModel : ViewModelWithParameter<TParameter>
        where TParameter : ViewModelParameters
    {

    }

    public interface IWay<TViewModel> : IWay
        where TViewModel : ViewModel
    {
    }

    public interface IWay
    {
        void Go();
    }
}