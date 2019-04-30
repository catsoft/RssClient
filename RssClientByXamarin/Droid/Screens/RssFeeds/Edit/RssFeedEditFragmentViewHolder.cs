using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.Edit
{
    public class RssFeedEditFragmentViewHolder
    {
        public RssFeedEditFragmentViewHolder([NotNull] View view)
        {
            SendButton = view.FindNotNull<Button>(Resource.Id.button_rssEdit_submit);
            TextInputLayout = view.FindNotNull<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);
        }

        [NotNull] public Button SendButton { get; }

        [NotNull] public TextInputLayout TextInputLayout { get; }

        [NotNull] public EditText EditText => TextInputLayout.EditText.NotNull();
    }
}
