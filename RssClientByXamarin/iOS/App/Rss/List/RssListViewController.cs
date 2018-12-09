using System.Threading.Tasks;
using iOS.App.Base.Table;
using Shared.App.Rss;

namespace iOS.App.Rss.List
{
	public class RssListViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private RssRepository _rssRepository;

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			_rssRepository = RssRepository.Instance;

			var list = await Task.Run(() => _rssRepository.GetList());

			List.AddRange(list);
		}
	}
}