using Android.OS;
using Android.Support.V4.App;
using Autofac;
using Shared;
using Shared.Infrastructure.ViewModels;

namespace Droid.Screens.Base
{
    public class ViewModelFragment<TViewModel> : Fragment
        where TViewModel : ViewModel
    {
        public TViewModel ViewModel { get; private set; }
        
        public ViewModelFragment()
        {
            
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = App.Container.Resolve<ViewModelProvider>().Resolve<TViewModel>();
        }
    }
}