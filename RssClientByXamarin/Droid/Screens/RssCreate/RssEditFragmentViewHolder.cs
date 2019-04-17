using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace Droid.Screens.RssCreate
{
    public class RssCreateFragmentViewHolder
    {
        public RssCreateFragmentViewHolder(View view)
        {
            SendButton = view.FindViewById<Button>(Resource.Id.button_rssCreate_submit);
            TextInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssCreate_link);
        }

        public Button SendButton { get; }
        
        public TextInputLayout TextInputLayout { get; }

        public EditText EditText => TextInputLayout.EditText;
    }
}