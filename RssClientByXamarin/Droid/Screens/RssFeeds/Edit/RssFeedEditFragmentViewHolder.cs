using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using JetBrains.Annotations;

namespace Droid.Screens.RssFeeds.Edit
{
    public class RssFeedEditFragmentViewHolder
    {
        public RssFeedEditFragmentViewHolder([NotNull] View view)
        {
            SendButton = view.FindViewById<Button>(Resource.Id.button_rssEdit_submit).NotNull();
            TextInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link).NotNull();
        }

        [NotNull] public Button SendButton { get; }

        [NotNull] public TextInputLayout TextInputLayout { get; }

        [NotNull] public EditText EditText => TextInputLayout.EditText.NotNull();
    }
}
