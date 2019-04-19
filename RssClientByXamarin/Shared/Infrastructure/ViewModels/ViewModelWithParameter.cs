namespace Shared.Infrastructure.ViewModels
{
    public class ViewModelWithParameter<TParameter> : ViewModel
        where TParameter : ViewModelParameters
    {
        public ViewModelWithParameter(TParameter parameters) { Parameters = parameters; }

        public TParameter Parameters { get; }
    }
}
