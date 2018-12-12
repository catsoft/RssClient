using Database.Rss;
using iOS.App.Base.Table;
using iOS.App.Rss.List;
using iOS.App.Styles;
using Shared.App.Rss;

namespace iOS.App.Rss.Edit
{
	public class RssEditViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private readonly RssModel _item;
		private readonly RssRepository _rssRepository;

		public RssEditViewController(RssModel item)
		{
			_item = item;
			_rssRepository = RssRepository.Instance;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssEditTitle;
			}
		}
	}
}