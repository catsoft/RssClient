using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;

namespace Droid.Screens.Navigation
{
    public abstract class BaseFragment : InjectFragment
    {
        protected abstract void RestoreState(Bundle saved);
        protected abstract int LayoutId { get; }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if(savedInstanceState != null)
                RestoreState(savedInstanceState);
            
            var view = inflater.Inflate(LayoutId, container, false);

            var color = ContextCompat.GetColor(Context, Resource.Color.fragment_background);
            view.SetBackgroundColor(new Color(color));

            return view;
        }
    }
}