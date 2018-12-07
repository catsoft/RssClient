using iOS.App.Base.Table;
using Shared.App.Rss;
using UIKit;

namespace iOS.App.Rss.List
{
	public class RssViewCell : BaseTableViewCell<RssModel>
	{
		public RssViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
		}

		public override void BindData(RssModel item)
		{
		}
	}
}