using Android.OS;
using Core.ViewModels.Messages.Book;
using Droid.Screens.Navigation;

namespace Droid.Screens.Messages.Book
{
    public class BookMessagesFragment : BaseFragment<BookMessagesViewModel>
    {
        protected override int LayoutId { get; }
        public override bool IsRoot => false;
        protected override void RestoreState(Bundle saved)
        {
            throw new System.NotImplementedException();
        }
    }
}