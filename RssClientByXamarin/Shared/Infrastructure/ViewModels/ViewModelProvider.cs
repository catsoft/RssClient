using Autofac;

namespace Shared.Infrastructure.ViewModels
{
    public class ViewModelProvider
    {
        public ViewModelProvider()
        {
            
        }

        public TViewModel Resolve<TViewModel>()
        {
            return App.Container.Resolve<TViewModel>();
        }
    }
}