using System;
using Android.Content;
using Android.Views;
using Core.Extensions;
using Core.Services.RssMessages;
using DynamicData;
using JetBrains.Annotations;
using ReactiveUI.AndroidSupport;

namespace Droid.Screens.Messages.Book
{
    //TODO separate to all messages and feed messages
    public class BookViewPagerAdapterHolder
    {
        private readonly Context _context;
        private readonly SourceList<RssMessageServiceModel> _sourceList;

        [NotNull] public ReactivePagerAdapter<RssMessageServiceModel> Adapter { get; }
            
        public BookViewPagerAdapterHolder(Context context, IObservable<IChangeSet<RssMessageServiceModel>> changeSet, SourceList<RssMessageServiceModel> sourceList)
        {
            _context = context;
            _sourceList = sourceList;
            Adapter = new ReactivePagerAdapter<RssMessageServiceModel>(changeSet, ViewCreator, ViewInitializer);
        }

        private void ViewInitializer([NotNull] RssMessageServiceModel model, [NotNull] View view)
        {
            var viewHolder = new BookMessageViewHolder(view);
            viewHolder.Bind(model);
            viewHolder.SetCounting(_sourceList.NotNull().Items.IndexOf(model), Adapter.Count);
        }

        private View ViewCreator(RssMessageServiceModel message, ViewGroup parent)
        {
            var view = LayoutInflater.From(_context).NotNull().Inflate(Resource.Layout.view_book_message, parent, false);

            return view;
        }
    }
}