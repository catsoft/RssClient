using Android.Views;
using Android.Widget;

namespace Droid.Screens.Contacts
{
    public class ContactItemViewHolder
    {
        public ContactItemViewHolder(View view)
        {
            IconImageView = view.FindViewById<ImageView>(Resource.Id.imageView_linkElementContacts_icon);
            TitleTextView = view.FindViewById<TextView>(Resource.Id.textView_linkElementContacts_title);
            RootView = view;
        }

        public View RootView { get; }
        
        public ImageView IconImageView { get; }
        
        public TextView TitleTextView { get; }
    }
}