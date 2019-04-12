namespace Shared.Infrastructure.ViewModels
{
    public class ViewModelWithParameter<TParameter> : ViewModel
    where TParameter : ViewModelParameters
    {
        public TParameter Parameterse { get; }
        
        public ViewModelWithParameter(TParameter parameterse)
        {
            Parameterse = parameterse;
        }
    }
}