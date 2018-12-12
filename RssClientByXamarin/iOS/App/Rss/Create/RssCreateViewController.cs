using Database.Rss;
using iOS.App.Base.Table;
using iOS.App.Rss.List;
using iOS.App.Styles;
using Shared.App.Rss;

namespace iOS.App.Rss.Create
{
	public class RssCreateViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private readonly RssRepository _rssRepository;

		public RssCreateViewController()
		{
			_rssRepository = RssRepository.Instance;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssCreateTitle;
			}
		}
	}
}