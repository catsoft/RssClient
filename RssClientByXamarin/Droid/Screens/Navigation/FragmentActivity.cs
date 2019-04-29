using System.Linq;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Autofac;
using Core;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Core.Repositories.Configurations;

namespace Droid.Screens.Navigation
{
    public abstract class FragmentActivity<TViewModel> : BurgerActivity<TViewModel>, IFragmentManager
        where TViewModel : ViewModel
    {
        private IConfigurationRepository _configurationRepository;

        protected abstract int ContainerId { get; }

        public void AddFragment(Fragment fragment)
        {
            var container = FindViewById(ContainerId);

            var config = _configurationRepository.GetSettings<AppConfiguration>();
            
            var fragmentManager = new FragmentNavigator(SupportFragmentManager, config, (container as ViewGroup).NotNull());

            fragmentManager.GoTo(fragment);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _configurationRepository = App.Container.Resolve<IConfigurationRepository>();

            SupportFragmentManager.BackStackChanged += (sender, args) =>
            {
                var lastFragment = SupportFragmentManager.Fragments.LastOrDefault();
                if (lastFragment is ITitle titleFragment) Title = titleFragment.Title;

                UpdateDrawerState();
            };
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout.IsDrawerOpen(DrawerGravity))
            {
                DrawerLayout.CloseDrawer(DrawerGravity);
            }
            else
            {
                if (IsHomeToggle)
                    Finish();
                else
                    base.OnBackPressed();
            }
        }
    }
}
