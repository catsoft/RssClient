using System;
using System.Collections.Generic;
using Android.App;
using Droid.Screens.Base.Adapters;
using JetBrains.Annotations;
using Shared.Database.Rss;

namespace Droid.Screens.RssMessagesList
{
    public abstract class BaseRssMessagesAdapter : DataBindAdapter<RssMessageServiceModel, IEnumerable<RssMessageServiceModel>, RssMessagesListViewHolder>
    {
        public BaseRssMessagesAdapter([NotNull] IEnumerable<RssMessageServiceModel> items, [NotNull] Activity activity) : base(items, activity)
        {
        }
        
        public virtual event EventHandler<RssMessageServiceModel> Click;
        public virtual event EventHandler<RssMessageServiceModel> LongClick;
        public virtual event EventHandler<RssMessageServiceModel> LeftButtonClick;
        public virtual event EventHandler<RssMessageServiceModel> RightButtonClick;
    }
}