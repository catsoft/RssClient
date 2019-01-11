using System;
using System.Collections.Generic;
using Analytics.Rss;
using iOS.Screens.Base.Stated;
using iOS.Styles;
using UIKit;

namespace iOS.Screens.Base.Table
{
	public class BaseTableViewController<TTableCell, TItem, TItemsCollection> : UITableViewController
		where TTableCell : BaseTableViewCell<TItem>
        where TItemsCollection : IEnumerable<TItem>
        where TItem : class
	{
		public BaseTableViewSource<TTableCell, TItem, TItemsCollection> Source { get; set; }
		public StatedViewControllerDecorator StatedDecorator { get; private set; }

		public event Action RefresherValueChanged;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Source = new BaseTableViewSource<TTableCell, TItem, TItemsCollection>(UITableViewCellStyle.Default);

			TableView.RowHeight = UITableView.AutomaticDimension;
			TableView.EstimatedRowHeight = 100;
			TableView.Source = Source;
			TableView.BackgroundColor = Colors.CommonBack;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			var refresher = new UIRefreshControl();
			refresher.ValueChanged += (sender, args) => RefresherValueChanged?.Invoke();
			refresher.TintColor = Colors.PrimaryColor;
			TableView.Add(refresher);

			StatedDecorator = new StatedViewControllerDecorator(this);
			StatedDecorator.SetNormal(new NormalData());

            ScreenLog.Instance.TrackScreenOpen(GetType());
        }
    }
}