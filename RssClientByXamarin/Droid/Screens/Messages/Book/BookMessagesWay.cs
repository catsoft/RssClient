using Core.Infrastructure.Navigation;
using Core.ViewModels.Messages.Book;
using Droid.Screens.Navigation;
using JetBrains.Annotations;

namespace Droid.Screens.Messages.Book
{
    public class BookMessagesWay : IWayWithParameters<BookMessagesViewModel, BookMessagesParameter>
    {
        [NotNull] private readonly IFragmentManager _fragmentManager;
        private readonly BookMessagesParameter _parameter;

        public BookMessagesWay([NotNull] IFragmentManager fragmentManager, BookMessagesParameter parameter)
        {
            _fragmentManager = fragmentManager;
            _parameter = parameter;
        }
        
        public void Go()
        {
            var fragment = new BookMessagesFragment();
            fragment.SetParameters(_parameter);
            
            _fragmentManager.AddFragment(fragment);
        }
    }
}