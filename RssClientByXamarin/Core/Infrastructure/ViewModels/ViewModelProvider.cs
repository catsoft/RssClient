using Autofac;
using Core.Extensions;
using JetBrains.Annotations;

namespace Core.Infrastructure.ViewModels
{
    public class ViewModelProvider
    {
        [NotNull]
        public TViewModel Resolve<TViewModel>([CanBeNull] ViewModelParameters parameters = null)
            where TViewModel : ViewModel
        {
            if (parameters != null)
            {
                var typedParameter = new TypedParameter(parameters.GetType(), parameters);
                return App.Container.Resolve<TViewModel>(typedParameter).NotNull();
            }

            return App.Container.Resolve<TViewModel>().NotNull();
        }
    }
}
