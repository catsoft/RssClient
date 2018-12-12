using Database.Rss;
using Foundation;
using iOS.App.Base.Table;
using iOS.App.Styles;
using SDWebImage;
using UIKit;

namespace iOS.App.Rss.Detail
{
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
				Spacing = 5
			};

			_dateLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Font = UIFont.SystemFontOfSize(16)
			};

			_titleLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Lines = 0,
				Font = UIFont.FromName("Helvetica-bold", 18),
			};

			_contentLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Lines = 0,
				Font = UIFont.SystemFontOfSize(14),
			};

			_imageContentView = new UIImageView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ContentMode = UIViewContentMode.ScaleAspectFit,
			};

			RootView.AddSubview(_rootStackView);

			_rootStackView.AddArrangedSubview(_dateLabel);
			_rootStackView.AddArrangedSubview(_titleLabel);
			_rootStackView.AddArrangedSubview(_contentLabel);
			_rootStackView.AddArrangedSubview(_imageContentView);
		}

		public override void BindData(RssMessageModel item)
		{
			_dateLabel.Text = item.CreationDate.ToString("g");
			_titleLabel.Text = item.Title;
			_contentLabel.Text = item.Text;
			_imageContentView.SetImage(new NSUrl(item.ImageUrl ?? ""));
		}

		public override void UpdateConstraints()
		{
			base.UpdateConstraints();

			if (_shouldSetupConstraint)
			{
				RootView.TopAnchor.ConstraintEqualTo(_rootStackView.TopAnchor, -Dimensions.CommonTopMargin).Active = true;
				RootView.LeftAnchor.ConstraintEqualTo(_rootStackView.LeftAnchor, -Dimensions.CommonLeftMargin).Active = true;
				RootView.BottomAnchor.ConstraintEqualTo(_rootStackView.BottomAnchor, Dimensions.CommonBottomMargin).Active = true;
				RootView.RightAnchor.ConstraintEqualTo(_rootStackView.RightAnchor, Dimensions.CommonRightMargin).Active = true;

				_shouldSetupConstraint = false;
			}
		}
	}
}