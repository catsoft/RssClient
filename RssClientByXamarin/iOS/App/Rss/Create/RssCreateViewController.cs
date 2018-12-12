using Database.Rss;
using iOS.App.Base.Table;
using iOS.App.Rss.List;
using iOS.App.Styles;
using Shared.App.Rss;

namespace iOS.App.Rss.Create
{
	public class RssCreateViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private RssRepository _rssRepository;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_rssRepository = RssRepository.Instance;

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssCreateTitle;
			}
		}
	}
}