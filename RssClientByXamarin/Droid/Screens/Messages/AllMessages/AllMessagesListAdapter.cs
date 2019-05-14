using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Core.Configuration.Settings;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessages
{
    public class AllMessagesListAdapter : BaseAllMessagesListAdapter
    {
        [NotNull] private readonly AppConfiguration _appConfiguration;

        public AllMessagesListAdapter([NotNull] Activity activity, [NotNull] AppConfiguration appConfiguration) : base(activity, appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var viewHolder = base.OnCreateViewHolder(parent, viewType);

            if (viewHolder is AllMessageListItemViewHolder allMessageListItemViewHolder)
            {
                allMessageListItemViewHolder.IsShowContent = _appConfiguration.ReaderType == ReaderType.Strip;
            }
            
            return viewHolder;
        }
    }
}