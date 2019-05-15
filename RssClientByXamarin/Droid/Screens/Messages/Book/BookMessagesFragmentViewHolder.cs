using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.Book
{
    public class BookMessagesFragmentViewHolder
    {
        public BookMessagesFragmentViewHolder([NotNull] View view)
        {
            ViewPager = view.FindNotNull<ViewPager>(Resource.Id.viewPager_bookMessages_pager);
            EmptyTextView = view.FindNotNull<TextView>(Resource.Id.textView_bookMessages_emptyText);
        }
        
        [NotNull] public ViewPager ViewPager { get; }
        
        [NotNull] public TextView EmptyTextView { get; }
    }
}