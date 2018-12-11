using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace iOS.App.Base.Table
{
	public class BaseTableViewSource<TTableCell, TItem> : UITableViewSource
		where TTableCell : BaseTableViewCell<TItem>
		where TItem : class
	{
		public event Action<TItem> ItemSelected;
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

			if (cell != null)
			{
				cell.ClipsToBounds = false;
				cell.Layer.MasksToBounds = false;

				cell.BindData(item);

				cell.UpdateConstraints();
			}

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			ItemSelected?.Invoke(_items[indexPath.Row]);
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}
	}
}