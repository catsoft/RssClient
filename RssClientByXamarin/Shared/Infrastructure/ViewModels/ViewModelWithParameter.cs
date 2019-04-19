using JetBrains.Annotations;

namespace Shared.Infrastructure.ViewModels
{
    public class ViewModelWithParameter<TParameter> : ViewModel
        where TParameter : ViewModelParameters
    {
        public ViewModelWithParameter([NotNull] TParameter parameters) { Parameters = parameters; }

        [NotNull] public TParameter Parameters { get; }
    }
}
