using System;
using System.Collections.Generic;
using System.Linq;
using Database.Rss;
using Foundation;
using UIKit;

namespace iOS.App.Base.Table
{
	public class BaseTableViewSource<TTableCell, TItem, TItemsCollection> : UITableViewSource
		where TTableCell : BaseTableViewCell<TItem>
        where TItemsCollection : IEnumerable<TItem>
        where TItem : class
	{
		public event Action<TItem> ItemSelected;
        private TItemsCollection _items;
		private readonly FactoryTableViewCellFactory<TTableCell, TItem> _factory;

        public int ItemsCount => _items.Count();

		public BaseTableViewSource(UITableViewCellStyle style)
		{
			_factory = new FactoryTableViewCellFactory<TTableCell, TItem>(style);
		}

		public void SetList(TItemsCollection list)
        {
            _items = list;
        }

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return _items.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cellIdentifier = nameof(TTableCell);
			var cell = (TTableCell)tableView.DequeueReusableCell(cellIdentifier) ?? _factory.Create();

			var item = _items.ElementAt(indexPath.Row);

			cell.BindData(item);

			cell.UpdateConstraints();

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var item = _items.ElementAt(indexPath.Row);
			ItemSelected?.Invoke(item);
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}
	}
}