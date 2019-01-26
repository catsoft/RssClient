using Android.Support.V4.App;
using Droid.Container;

namespace Droid.Screens.Navigation
{
    public abstract class InjectFragment : Fragment
    {
        protected InjectFragment()
        {
            this.Inject();
        }
    }
}