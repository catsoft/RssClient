using System;
using System.Linq;
using Android.Animation;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Droid.Screens.Base;
using Java.Lang;

namespace Droid.Screens.Navigation
{
    public abstract class BurgerActivity : ToolbarActivity, NavigationView.IOnNavigationItemSelectedListener,
        View.IOnClickListener
    {
        protected bool IsHomeToggle = true;
        
        public ActionBarDrawerToggle Toggle { get; private set; }

        protected DrawerLayout DrawerLayout { get; private set; }
        protected NavigationView NavigationView { get; private set; }
        protected int DrawerGravity { get; } = (int) GravityFlags.Start;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            Toggle = new ActionBarDrawerToggle(this, DrawerLayout, Toolbar, Resource.String.navigation_drawer_open,
                Resource.String.navigation_drawer_close);
            DrawerLayout.AddDrawerListener(Toggle);
            Toolbar.SetNavigationOnClickListener(this);
            Toggle.SyncState();

            NavigationView = FindViewById<NavigationView>(Resource.Id.navigation_view_all);
            NavigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout.IsDrawerOpen(DrawerGravity))
                DrawerLayout.CloseDrawer(DrawerGravity);
            else
                base.OnBackPressed();
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
            var isHome = (SupportFragmentManager.Fragments.LastOrDefault() as TitleFragment)?.RootFragment == true;

            if (IsHomeToggle != isHome)
            {
                IsHomeToggle = !IsHomeToggle;
                    
                var from = !isHome ? 0 : 1;
                var to = !isHome ? 1 : 0;
                var anim = ValueAnimator.OfFloat(from, to);
                anim.Update += (sender, args) =>
                {
                    var offset = args.Animation.AnimatedValue as Float;
                    var value = offset.FloatValue();
                    Console.WriteLine(value);
                    Toggle.OnDrawerSlide(DrawerLayout, value);
                };
                anim.SetInterpolator(new LinearInterpolator());
                anim.SetDuration(500);
                anim.Start();

                DrawerLayout.SetDrawerLockMode(isHome
                    ? DrawerLayout.LockModeLockedClosed
                    : DrawerLayout.LockModeUnlocked);
            }
        }
    }
}