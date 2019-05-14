using Android.App;
using Core.Configuration.Settings;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.AllMessages
{
    public class FavoriteMessagesListAdapter : BaseAllMessagesListAdapter
    {
        public FavoriteMessagesListAdapter([NotNull] Activity activity, [NotNull] AppConfiguration appConfiguration) : base(activity, appConfiguration)
        {
        }
    }
}