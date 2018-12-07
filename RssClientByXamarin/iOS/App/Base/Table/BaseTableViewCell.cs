using UIKit;

namespace iOS.App.Base.Table
{
	public abstract class BaseTableViewCell<TItem> : UITableViewCell
		where TItem : class 
	{
		protected BaseTableViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
		}

		public abstract void BindData(TItem item);
	}
}