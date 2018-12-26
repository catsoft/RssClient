using iOS.App.RssScreens.Detail;
using iOS.App.RssScreens.List;
using UIKit;

namespace iOS.App.Base.Table
{
	public class FactoryTableViewCellFactory<TTableCell, TItem>
		where TTableCell : BaseTableViewCell<TItem>
		where TItem : class
	{
		private readonly UITableViewCellStyle _style;

		public FactoryTableViewCellFactory(UITableViewCellStyle style)
		{
			_style = style;
		}

		public TTableCell Create()
		{
			if (typeof(TTableCell) == typeof(RssViewCell))
			{
				return new RssViewCell(_style, nameof(RssViewCell)) as TTableCell;
			}

			if (typeof(TTableCell) == typeof(RssMessageViewCell))
			{
				return new RssMessageViewCell(_style, nameof(RssMessageViewCell)) as TTableCell;
			}

			return null;
		}
	}
}