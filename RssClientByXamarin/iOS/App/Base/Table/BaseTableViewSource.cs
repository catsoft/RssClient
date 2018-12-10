using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace iOS.App.Base.Table
{
	public class BaseTableViewSource<TTableCell, TItem> : UITableViewDataSource
		where TTableCell : BaseTableViewCell<TItem>
		where TItem : class 
	{
		private readonly List<TItem> _items;
		private readonly FactoryTableViewCellFactory<TTableCell, TItem> _factory;

		public BaseTableViewSource(List<TItem> items, UITableViewCellStyle style)
		{
			_items = items;
			_factory = new FactoryTableViewCellFactory<TTableCell, TItem>(style);
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return _items.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cellIdentifier = nameof(TTableCell);
			var cell = (tableView.DequeueReusableCell(cellIdentifier) ?? _factory.Create()) as TTableCell;

			var item = _items[indexPath.Row];

			cell?.BindData(item);

			cell?.UpdateConstraints();

			return cell;
		}
	}
}