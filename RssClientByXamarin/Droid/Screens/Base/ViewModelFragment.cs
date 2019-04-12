using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Autofac;
using ReactiveUI;
using Shared;
using Shared.Infrastructure.ViewModels;

namespace Droid.Screens.Base
{
    public class ViewModelFragment<TViewModel> : LifeCycleFragment<TViewModel>
        where TViewModel : ViewModel
    {
        private ViewModelParameters _parameters;

        public ViewModelFragment()
        {

        }

        public void SetParameters(ViewModelParameters parameters)
        {
            _parameters = parameters;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = App.Container.Resolve<ViewModelProvider>().Resolve<TViewModel>(_parameters);
        }
    }
}