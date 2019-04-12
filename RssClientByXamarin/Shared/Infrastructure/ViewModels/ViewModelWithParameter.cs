namespace Shared.Infrastructure.ViewModels
{
    public class ViewModelWithParameter<TParameter> : ViewModel
    where TParameter : ViewModelParameters
    {
        public TParameter Parameters { get; }
        
        public ViewModelWithParameter(TParameter parameters)
        {
            Parameters = parameters;
        }
    }
}