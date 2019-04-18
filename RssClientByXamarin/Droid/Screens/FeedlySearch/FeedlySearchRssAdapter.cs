using System;
using System.Collections.Generic;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Droid.Screens.Base.Adapters;
using Shared.Configuration.Settings;
using Shared.Repository.Feedly;

namespace Droid.Screens.FeedlySearch
{
    public class FeedlySearchRssAdapter : DataBindAdapter<FeedlyRssDomainModel, IEnumerable<FeedlyRssDomainModel>, FeedlyRssViewHolder>
    {
        public event EventHandler<FeedlyRssDomainModel> ClickAddImage; 
        
        private readonly AppConfiguration _appConfiguration;
        
        public FeedlySearchRssAdapter(Activity activity, AppConfiguration appConfiguration) : base(new List<FeedlyRssDomainModel>(), activity)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_item_feedly_rss, parent, false);
            
            var viewHolder = new FeedlyRssViewHolder(view, _appConfiguration.LoadAndShowImages);

            viewHolder.AddImageView.Click += (sender, args) => ClickAddImage?.Invoke(sender, viewHolder.Item);
            
            return viewHolder;
        }
    }
}