using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.Create
{
    public class RssFeedCreateFragmentViewHolder
    {
        public RssFeedCreateFragmentViewHolder([NotNull] View view)
        {
            SendButton = view.FindNotNull<Button>(Resource.Id.button_rssCreate_submit);
            TextInputLayout = view.FindNotNull<TextInputLayout>(Resource.Id.textInputLayout_rssCreate_link);
        }

        [NotNull] public Button SendButton { get; }

        [NotNull] public TextInputLayout TextInputLayout { get; }

        [NotNull] public EditText EditText => TextInputLayout.EditText.NotNull();
    }
}
