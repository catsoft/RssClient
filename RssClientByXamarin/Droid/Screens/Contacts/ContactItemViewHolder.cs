using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Contacts
{
    public class ContactItemViewHolder
    {
        public ContactItemViewHolder([NotNull] View view)
        {
            IconImageView = view.FindNotNull<ImageView>(Resource.Id.imageView_linkElementContacts_icon);
            TitleTextView = view.FindNotNull<TextView>(Resource.Id.textView_linkElementContacts_title);
            RootView = view;
        }

        [NotNull] public View RootView { get; }

        [NotNull] public ImageView IconImageView { get; }

        [NotNull] public TextView TitleTextView { get; }
    }
}
