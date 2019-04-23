using Android.OS;
using Autofac;
using Core;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using JetBrains.Annotations;

namespace Droid.Screens.Base
{
    public class ViewModelFragment<TViewModel> : LifeCycleFragment<TViewModel>
        where TViewModel : ViewModel
    {
        [CanBeNull] private ViewModelParameters _parameters;

        [NotNull]
        public new TViewModel ViewModel
        {
            [NotNull] get => base.ViewModel.NotNull();
            private set => base.ViewModel = value;
        }

        public void SetParameters([CanBeNull] ViewModelParameters parameters) { _parameters = parameters; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = App.Container.Resolve<ViewModelProvider>().NotNull().Resolve<TViewModel>(_parameters);
        }
    }
}
