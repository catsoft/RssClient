using iOS.App.Base.Table;
using iOS.App.Rss.List;
using Shared.App.Rss;

namespace iOS.App.Rss.Detail
{
	public class RssDetailViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private readonly RssModel _item;
		private RssRepository _rssRepository;

		public RssDetailViewController(RssModel item)
		{
			_item = item;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_rssRepository = RssRepository.Instance;

			if (NavigationItem != null)
			{
				NavigationItem.Title = _item?.Name;
			}
		}
	}
}