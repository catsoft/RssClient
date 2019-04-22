using Android.App;
using Android.Support.V7.Widget;
using JetBrains.Annotations;

namespace Droid.Screens.Base.Adapters
{
    public abstract class WithActivityAdapter : RecyclerView.Adapter
    {
        protected WithActivityAdapter([NotNull] Activity activity) { Activity = activity; }

        [NotNull] protected Activity Activity { get; }
    }
}
