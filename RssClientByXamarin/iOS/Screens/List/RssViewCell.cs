using Database.Rss;
using Foundation;
using iOS.Screens.Base.Table;
using iOS.Styles;
using Repository;
using SDWebImage;
using UIKit;

namespace iOS.Screens.List
{
	public class RssViewCell : BaseTableViewCell<RssModel>
	{
		private bool _shouldSetupConstraint = true;
        private RssMessagesRepository _rssMessagesRepository;

		private readonly UIStackView _rootStackView;
		private readonly UIImageView _imagePreview;
		private readonly UIStackView _textStackView;
		private readonly UILabel _nameLabel;
		private readonly UILabel _dataUpdateLabel;
		private readonly UILabel _countMessages;
        private NSLayoutConstraint _countMessageWidthConstraint;

        public RssViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
            _rssMessagesRepository = RssMessagesRepository.Instance;

            _rootStackView = new UIStackView()
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
                Distribution = UIStackViewDistribution.FillProportionally
			};

			_nameLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Font = UIFont.FromName("Helvetica-bold", 16),
                Lines = 0,
			};

			_dataUpdateLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Font = UIFont.SystemFontOfSize(14),
                Lines = 1,
                LineBreakMode = UILineBreakMode.HeadTruncation,
			};

			_countMessages = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				TextAlignment = UITextAlignment.Right,
			};


			RootView.AddSubview(_rootStackView);
			_rootStackView.AddArrangedSubview(_imagePreview);
			_rootStackView.AddArrangedSubview(_textStackView);
			_rootStackView.AddArrangedSubview(_countMessages);

			_textStackView.AddArrangedSubview(_nameLabel);
			_textStackView.AddArrangedSubview(_dataUpdateLabel);
		}

		public override void BindData(RssModel item)
		{
			_nameLabel.Text = item.Name;
			_dataUpdateLabel.Text = item.UpdateTime == null ? "Не обновлено" : $"Обновлено: {item.UpdateTime.Value:g}";
            _countMessages.Text = _rssMessagesRepository.GetCountForModel(item).ToString();
			var placeHolderImage = UIImage.FromBundle("EmptyImage").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			_imagePreview.SetImage(new NSUrl(item.UrlPreviewImage ?? ""), placeHolderImage);
			_imagePreview.TintColor = Colors.PrimaryColor;
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

				_imagePreview.HeightAnchor.ConstraintEqualTo(42).Active = true;
				_imagePreview.WidthAnchor.ConstraintEqualTo(42).Active = true;

                _dataUpdateLabel.SetContentCompressionResistancePriority((int)UILayoutPriority.Required, UILayoutConstraintAxis.Horizontal);

                _shouldSetupConstraint = false;
			}

            _countMessages.SizeToFit();
            var width = _countMessages.Frame.Width;

            if (_countMessageWidthConstraint == null)
            {
                _countMessageWidthConstraint = _countMessages.WidthAnchor.ConstraintEqualTo(width);
                _countMessageWidthConstraint.Active = true;
            }

            _countMessageWidthConstraint.Constant = width;
        }
	}
}