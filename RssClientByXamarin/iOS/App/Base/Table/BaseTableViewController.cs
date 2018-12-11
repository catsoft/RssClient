using System;
using System.Collections.Generic;
using Foundation;
using iOS.App.Base.Stated;
using iOS.App.Rss.Detail;
using iOS.App.Styles;
using UIKit;

namespace iOS.App.Base.Table
{
	public class BaseTableViewController<TTableCell, TItem> : UITableViewController
		where TTableCell : BaseTableViewCell<TItem>
		where TItem : class
	{
		protected List<TItem> List = new List<TItem>();
		public BaseTableViewSource<TTableCell, TItem> Source { get; set; }
		public StatedViewControllerDecorator StatedDecorator { get; private set; }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Source = new BaseTableViewSource<TTableCell, TItem>(List, UITableViewCellStyle.Default);

			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 100;
			TableView.Source = Source;
			TableView.BackgroundColor = Colors.CommonBack;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			StatedDecorator = new StatedViewControllerDecorator(this);
			StatedDecorator.SetNormal(new NormalData());
		}

	}
}