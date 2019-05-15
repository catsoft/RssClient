using System;
using Android.Content;
using Android.Views;
using Core.Services.RssMessages;
using DynamicData;
using ReactiveUI.AndroidSupport;

namespace Droid.Screens.Messages.Book
{
    public class BookViewPagerAdapterHolder
    {
        private readonly Context _context;
        
        public ReactivePagerAdapter<RssMessageServiceModel> Adapter { get; }
            
        public BookViewPagerAdapterHolder(Context context, IObservable<IChangeSet<RssMessageServiceModel>> changeSet)
        {
            _context = context;
            Adapter = new ReactivePagerAdapter<RssMessageServiceModel>(changeSet, ViewCreator);
        }

        private View ViewCreator(RssMessageServiceModel message, ViewGroup parent)
        {
            var view = LayoutInflater.From(_context).Inflate(Resource.Layout.view_book_message, parent, false);

            var viewHolder = new BookMessageViewHolder(view);
            viewHolder.Bind(message);
            
            return view;
        }
    }
}