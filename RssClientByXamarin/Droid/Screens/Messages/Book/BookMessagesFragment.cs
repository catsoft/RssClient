using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Core.Extensions;
using Core.ViewModels.Messages.Book;
using Droid.NativeExtension;
using Droid.Screens.Navigation;
using ReactiveUI;

namespace Droid.Screens.Messages.Book
{
    public class BookMessagesFragment : BaseFragment<BookMessagesViewModel>
    {
        private BookMessagesFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_book_messages;
        
        public override bool IsRoot => false;
        
        protected override void RestoreState(Bundle saved)
        {
        }

        // ReSharper disable once EmptyConstructor
        public BookMessagesFragment()
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _viewHolder = new BookMessagesFragmentViewHolder(view);
            
            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(w => w.ListViewModel.IsEmpty)
                    .Select(w => w.ToVisibility())
                    .BindTo(_viewHolder.EmptyTextView, textView => textView.Visibility)
                    .AddTo(disposable);
                
                var adapterHolder = new BookViewPagerAdapterHolder(Activity, ViewModel.ListViewModel.ConnectChanges);
                _viewHolder.ViewPager.Adapter = adapterHolder.Adapter;

                ViewModel.LoadCommand.ExecuteIfCan();
            });
            
            return view;
        }
    }
}