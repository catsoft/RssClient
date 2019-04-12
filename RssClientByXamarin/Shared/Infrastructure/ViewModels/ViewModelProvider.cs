using Autofac;

namespace Shared.Infrastructure.ViewModels
{
    public class ViewModelProvider
    {
        public ViewModelProvider()
        {
            
        }

        public TViewModel Resolve<TViewModel>(ViewModelParameters parameters)
        {
            if (parameters != null)
            {
                var typedParameter = new TypedParameter(parameters.GetType(), parameters);
                return App.Container.Resolve<TViewModel>(typedParameter);
            }
            
            return App.Container.Resolve<TViewModel>();
        }
    }
}