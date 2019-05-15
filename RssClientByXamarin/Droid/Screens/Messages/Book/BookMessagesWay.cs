using Core.Infrastructure.Navigation;
using Core.ViewModels.Messages.Book;
using Droid.Screens.Navigation;

namespace Droid.Screens.Messages.Book
{
    public class BookMessagesWay : IWay<BookMessagesViewModel>
    {
        private readonly IFragmentManager _fragmentManager;

        public BookMessagesWay(IFragmentManager fragmentManager)
        {
            _fragmentManager = fragmentManager;
        }
        
        public void Go()
        {
            _fragmentManager.AddFragment(new BookMessagesFragment());
        }
    }
}