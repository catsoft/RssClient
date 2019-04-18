using System.Linq;
using Android.Animation;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Autofac;
using Droid.Repository.Configuration;
using Droid.Screens.Base;
using Java.Lang;
using Shared;
using Shared.Configuration.Settings;
using Shared.Infrastructure.ViewModels;

namespace Droid.Screens.Navigation
{
    public abstract class BurgerActivity<TViewModel> : BaseReactiveAppCompatActivity<TViewModel>, NavigationView.IOnNavigationItemSelectedListener,
        View.IOnClickListener 
        where TViewModel : ViewModel
    {
        private IConfigurationRepository _configurationRepository;

        protected bool IsHomeToggle { get; set; } = true;

        public ActionBarDrawerToggle Toggle { get; private set; }

        protected DrawerLayout DrawerLayout { get; private set; }
        protected NavigationView NavigationView { get; private set; }
        protected int DrawerGravity { get; } = (int) GravityFlags.Start;

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutBoolean(nameof(IsHomeToggle), IsHomeToggle);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _configurationRepository = App.Container.Resolve<IConfigurationRepository>();
            
            if (savedInstanceState != null)
            {
                IsHomeToggle = savedInstanceState.GetBoolean(nameof(IsHomeToggle));
            }

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            Toggle = new ActionBarDrawerToggle(this, DrawerLayout, Toolbar, Resource.String.navigation_drawer_open,
                Resource.String.navigation_drawer_close);
            DrawerLayout.AddDrawerListener(Toggle);
            Toolbar.SetNavigationOnClickListener(this);
            Toggle.SyncState();

            NavigationView = FindViewById<NavigationView>(Resource.Id.navigation_view_all);
            NavigationView.SetNavigationItemSelectedListener(this);

            Toggle.OnDrawerSlide(DrawerLayout, IsHomeToggle ? 0 : 1);
        }

        public abstract bool OnNavigationItemSelected(IMenuItem menuItem);

        public void OnClick(View v)
        {
            if (IsHomeToggle)
            {
                DrawerLayout.OpenDrawer(DrawerGravity);
            }
            else
                OnBackPressed();
        }

        protected void UpdateDrawerState()
        {
            var isHome = (SupportFragmentManager.Fragments.LastOrDefault() as IRoot)?.IsRoot== true;

            if (IsHomeToggle != isHome)
            {
                IsHomeToggle = !IsHomeToggle;

                var appConfiguration = _configurationRepository.GetSettings<AppConfiguration>();
                var time = appConfiguration.GetCalculationAnimationTime();

                var from = !isHome ? 0 : 1;
                var to = !isHome ? 1 : 0;
                var anim = ValueAnimator.OfFloat(from, to);
                anim.Update += (sender, args) =>
                {
                    var offset = (args.Animation.AnimatedValue as Float)?.FloatValue() ?? 0;
                    Toggle.OnDrawerSlide(DrawerLayout, offset);
                };
                anim.SetInterpolator(new LinearInterpolator());
                anim.SetDuration(time);
                anim.Start();

                DrawerLayout.SetDrawerLockMode(isHome
                    ? DrawerLayout.LockModeUnlocked
                    : DrawerLayout.LockModeLockedClosed);
            }
        }
    }
}