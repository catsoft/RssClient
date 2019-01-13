using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;

namespace Droid.Screens.Navigation
{
    public abstract class BurgerActivity : FragmentActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private ActionBarDrawerToggle _toggle;

        protected DrawerLayout DrawerLayout { get; private set; }
        protected NavigationView NavigationView{ get; private set; }
        protected int DrawerGravity { get; } = (int)GravityFlags.Start;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            _toggle = new ActionBarDrawerToggle(this, DrawerLayout, Toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            DrawerLayout.AddDrawerListener(_toggle);
            _toggle.SyncState();

            NavigationView = FindViewById<NavigationView>(Resource.Id.navigation_view_all);
            NavigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            if (DrawerLayout.IsDrawerOpen(DrawerGravity))
                DrawerLayout.CloseDrawer(DrawerGravity);
            else
                base.OnBackPressed();
        }

        public abstract bool OnNavigationItemSelected(IMenuItem menuItem);
    }
}