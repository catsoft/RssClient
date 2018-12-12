using Database.Rss;
using Foundation;
using iOS.App.Base.Table;
using iOS.App.Styles;
using SDWebImage;
using UIKit;

namespace iOS.App.Rss.List
{
	public class RssViewCell : BaseTableViewCell<RssModel>
	{
		private bool _shouldSetupConstraint = true;

		private readonly UIStackView _rootsStackView;
		private readonly UIImageView _imagePreview;
		private readonly UIStackView _textStackView;
		private readonly UILabel _nameLabel;
		private readonly UILabel _dataUpdateLabel;
		private readonly UILabel _countMessages;

		public RssViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
			_rootsStackView = new UIStackView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Spacing = 10,
				Axis = UILayoutConstraintAxis.Horizontal,
				Distribution = UIStackViewDistribution.Fill
			};

			_imagePreview = new UIImageView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_textStackView = new UIStackView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Spacing = 5,
				Axis = UILayoutConstraintAxis.Vertical,
			};

			_nameLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_dataUpdateLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_countMessages = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Right,
			};


			RootView.AddSubview(_rootsStackView);
			_rootsStackView.AddArrangedSubview(_imagePreview);
			_rootsStackView.AddArrangedSubview(_textStackView);
			_rootsStackView.AddArrangedSubview(_countMessages);

			_textStackView.AddArrangedSubview(_nameLabel);
			_textStackView.AddArrangedSubview(_dataUpdateLabel);
		}

		public override void BindData(RssModel item)
		{
			_nameLabel.Text = item.Name;
			_dataUpdateLabel.Text = item.UpdateTime == null ? "Не обновлено" : $"Обновлено: {item.UpdateTime.Value.ToString("g")}";
			_countMessages.Text = (item.Messages?.Count ?? 0).ToString();
			_imagePreview.SetImage(new NSUrl(item.UrlPreviewImage ?? ""));
		}

		public override void UpdateConstraints()
		{
			if (_shouldSetupConstraint)
			{
				RootView.TopAnchor.ConstraintEqualTo(_rootsStackView.TopAnchor, -Dimensions.CommonTopMargin).Active = true;
				RootView.LeftAnchor.ConstraintEqualTo(_rootsStackView.LeftAnchor, -Dimensions.CommonLeftMargin).Active = true;
				RootView.BottomAnchor.ConstraintEqualTo(_rootsStackView.BottomAnchor, Dimensions.CommonBottomMargin).Active = true;
				RootView.RightAnchor.ConstraintEqualTo(_rootsStackView.RightAnchor, Dimensions.CommonRightMargin).Active = true;

				_imagePreview.HeightAnchor.ConstraintEqualTo(48).Active = true;
				_imagePreview.WidthAnchor.ConstraintEqualTo(48).Active = true;

				_shouldSetupConstraint = false;
			}

			base.UpdateConstraints();
		}
	}
}