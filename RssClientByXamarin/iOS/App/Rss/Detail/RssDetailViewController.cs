using System;
using System.Threading.Tasks;
using Database.Rss;
using Foundation;
using iOS.App.Base.Stated;
using iOS.App.Base.Table;
using SDWebImage;
using Shared.App.Rss;
using Shared.App.Rss.RssDatabase;
using UIKit;

namespace iOS.App.Rss.Detail
{
	public class RssDetailViewController : BaseTableViewController<RssMessageViewCell, RssMessageModel>
	{
		private readonly RssModel _item;
		private readonly RssRepository _rssRepository;
		private readonly RssMessagesRepository _rssMessagesRepository;

		public RssDetailViewController(RssModel item)
		{
			_item = item;

			_rssRepository = RssRepository.Instance;
			_rssMessagesRepository = RssMessagesRepository.Instance;
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = _item?.Name;
			}

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

	public class RssMessageViewCell : BaseTableViewCell<RssMessageModel>
	{
		private bool _shouldSetupConstraint = true;
		private readonly UIStackView _rootStackView;

		private readonly UILabel _dateLabel;
		private readonly UILabel _titleLabel;
		private readonly UILabel _contentLabel;
		private readonly UIImageView _imageContentView;

		public RssMessageViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
			_rootStackView= new UIStackView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Axis = UILayoutConstraintAxis.Vertical,
				Spacing = 10
			};

			_dateLabel = new UILabel();
			_titleLabel = new UILabel();
			_contentLabel = new UILabel();
			_imageContentView = new UIImageView();

			RootView.AddSubview(_rootStackView);
			_rootStackView.AddSubview(_dateLabel);
			_rootStackView.AddSubview(_titleLabel);
			_rootStackView.AddSubview(_contentLabel);
			_rootStackView.AddSubview(_imageContentView);
		}

		public override void BindData(RssMessageModel item)
		{
			_dateLabel.Text = item.CreationDate.ToString("g");
			_titleLabel.Text = item.Title;
			_contentLabel.Text = item.Text;
			_imageContentView.SetImage(new NSUrl(item.Url ?? ""));
		}

		public override void UpdateConstraints()
		{
			base.UpdateConstraints();

			if (_shouldSetupConstraint)
			{
				_rootStackView.LeadingAnchor.ConstraintEqualTo(RootView.LeadingAnchor).Active = true;
				_rootStackView.TrailingAnchor.ConstraintEqualTo(RootView.TrailingAnchor).Active = true;
				_rootStackView.TopAnchor.ConstraintEqualTo(RootView.TopAnchor).Active = true;
				_rootStackView.BottomAnchor.ConstraintEqualTo(RootView.BottomAnchor).Active = true;

				_shouldSetupConstraint = false;
			}
		}
	}
}