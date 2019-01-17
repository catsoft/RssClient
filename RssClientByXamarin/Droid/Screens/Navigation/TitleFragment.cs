using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;

namespace Droid.Screens.Navigation
{
    public abstract class TitleFragment : Fragment
    {
        protected abstract int LayoutId { get; }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                if(Activity != null)
                    Activity.Title = _title;
            }
        }

        public abstract bool RootFragment { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(LayoutId, container, false);

            var color = ContextCompat.GetColor(Context, Resource.Color.fragment_background);
            view.SetBackgroundColor(new Color(color));

            return view;
        }
    }
}