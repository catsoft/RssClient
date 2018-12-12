using System.Threading.Tasks;
using Database.Rss;
using Foundation;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using SafariServices;
using Shared.App.Rss.RssDatabase;

namespace iOS.App.Rss.Detail
{
	public class RssDetailViewController : BaseTableViewController<RssMessageViewCell, RssMessageModel>
	{
		private readonly RssModel _item;
		private readonly RssMessagesRepository _rssMessagesRepository;

		public RssDetailViewController(RssModel item)
		{
			_item = item;

			_rssMessagesRepository = RssMessagesRepository.Instance;
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = _item?.Name;
			}

			Source.ItemSelected += model =>
			{
				var url = model.Url;
				var ctrl = new SFSafariViewController(new NSUrl(url ?? ""));
				PresentViewController(ctrl, true, null);
			};

			await LoadItems();
		}

		private async Task LoadItems()
		{
			StatedDecorator.SetLoading(new LoadingData());

			var items = await _rssMessagesRepository.GetMessagesForRss(_item);
			
			List.Clear();
			List.AddRange(items);

			TableView.ReloadData();

			StatedDecorator.SetNormal(new NormalData());
		}
	}
}