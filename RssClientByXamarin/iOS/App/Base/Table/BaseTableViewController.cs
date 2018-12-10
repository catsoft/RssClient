using System;
using System.Collections.Generic;
using Foundation;
using iOS.App.Base.Stated;
using iOS.App.Styles;
using UIKit;

namespace iOS.App.Base.Table
{
	public class BaseTableViewController<TTableCell, TItem> : UITableViewController
		where TTableCell : BaseTableViewCell<TItem>
		where TItem : class
	{
		protected List<TItem> List = new List<TItem>();
		public StatedViewControllerDecorator StatedDecorator { get; private set; }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new BaseTableViewSource<TTableCell, TItem>(List, UITableViewCellStyle.Default);

			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 100;
			TableView.DataSource = source;
			TableView.BackgroundColor = Colors.CommonBack;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			StatedDecorator = new StatedViewControllerDecorator(this);
			StatedDecorator.SetNormal(new NormalData());
		}
	}
}