using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Screens.Base.Adapters;
using Droid.Screens.Navigation;
using Shared.Database.Rss;

namespace Droid.Screens.RecommendedRssList
{
    public class RecommendedRssListFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_recommended;
        public override bool RootFragment => true;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var list = view.FindViewById<RecyclerView>();
            list.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            
            Title = GetText(Resource.String.recommended_title);

            return view;
        }
    }
}