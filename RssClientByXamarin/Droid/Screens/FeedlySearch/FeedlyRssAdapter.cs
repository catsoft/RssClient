using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Autofac;
using Droid.NativeExtension;
using Droid.Screens.Base.Adapters;
using Shared;
using Shared.Configuration.Settings;
using Shared.Repository.Feedly;
using Shared.Repository.Rss;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlyRssAdapter : DataBindAdapter<FeedlyRss, List<FeedlyRss>, FeedlyRssViewHolder>
    {
        private readonly AppConfiguration _appConfiguration;
        
        public FeedlyRssAdapter(List<FeedlyRss> items, Activity activity, AppConfiguration appConfiguration) : base(items, activity)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_feedly_rss, parent, false);
            
            var viewHolder = new FeedlyRssViewHolder(view, _appConfiguration.LoadAndShowImages);

            // TODO вынести
            viewHolder.AddImageView.Click += (sender, args) =>
            {
                var rssRepository = App.Container.Resolve<IRssRepository>();
                rssRepository.InsertByUrl(viewHolder.Item.FeedId);
                Activity.Toast(Activity.GetText(Resource.String.recommended_rss_add_rss_toast) + viewHolder.Item.Title);
            };
            
            return viewHolder;
        }
    }
}