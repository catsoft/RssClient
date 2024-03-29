using Android.OS;
using Android.Support.V7.Widget;
using Autofac;
using Core;
using Core.Analytics.Rss;
using Core.Infrastructure.ViewModels;
using Droid.Infrastructure.Theme;
using ReactiveUI.AndroidSupport;

namespace Droid.Screens.Base
{
    public abstract class BaseReactiveAppCompatActivity<TViewModel> : ReactiveAppCompatActivity<TViewModel>
        where TViewModel : ViewModel
    {
        protected abstract int ResourceView { get; }
        protected Toolbar Toolbar { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var themeController = App.Container.Resolve<AppThemeController>();
            themeController.SetTheme(this);

            SetContentView(ResourceView);

            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_toolbarAll_toolbar);
            if (Toolbar != null) SetSupportActionBar(Toolbar);

            // TODO add parametres or results long long task
            ViewModel = App.Container.Resolve<ViewModelProvider>().Resolve<TViewModel>();

            App.Container.Resolve<ScreenLog>().TrackScreenOpen(GetType());
        }

        public override bool OnSupportNavigateUp()
        {
            Finish();
            return true;
        }
    }
}
