using Android.App;
using Android.Support.V7.Widget;

namespace Droid.Screens.Base.Adapters
{
    public abstract class WithActivityAdapter : RecyclerView.Adapter
    {
        protected Activity Activity { get; }


        protected WithActivityAdapter(Activity activity)
        {
            Activity = activity;
        }
    }
}