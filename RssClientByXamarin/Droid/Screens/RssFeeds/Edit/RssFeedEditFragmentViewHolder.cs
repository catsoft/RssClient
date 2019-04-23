using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Droid.Resources;

namespace Droid.Screens.RssFeeds.Edit
{
    public class RssFeedEditFragmentViewHolder
    {
        public RssFeedEditFragmentViewHolder(View view)
        {
            SendButton = view.FindViewById<Button>(Resource.Id.button_rssEdit_submit);
            TextInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);
        }

        public Button SendButton { get; }

        public TextInputLayout TextInputLayout { get; }

        public EditText EditText => TextInputLayout.EditText;
    }
}
