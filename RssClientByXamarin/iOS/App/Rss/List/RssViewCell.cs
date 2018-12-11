using iOS.App.Base.Table;
using iOS.App.Styles;
using Shared.App.Rss;
using UIKit;

namespace iOS.App.Rss.List
{
	public class RssViewCell : BaseTableViewCell<RssModel>
	{
		private bool _shouldSetupConstraint = true;

		private readonly UIStackView _rootsStackView;
		private readonly UILabel _nameLabel;
		private readonly UILabel _urlLabel;

		public RssViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
			_rootsStackView = new UIStackView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Axis = UILayoutConstraintAxis.Vertical,
			};

			_nameLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_urlLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			RootView.AddSubview(_rootsStackView);
			_rootsStackView.AddArrangedSubview(_nameLabel);
			_rootsStackView.AddArrangedSubview(_urlLabel);
		}

		public override void BindData(RssModel item)
		{
			_nameLabel.Text = item.Name;
			_urlLabel.Text = item.Rss;
		}

		public override void UpdateConstraints()
		{
			if (_shouldSetupConstraint)
			{
				RootView.TopAnchor.ConstraintEqualTo(_rootsStackView.TopAnchor, -Dimensions.CommonTopMargin).Active = true;
				RootView.LeftAnchor.ConstraintEqualTo(_rootsStackView.LeftAnchor, -Dimensions.CommonLeftMargin).Active = true;
				RootView.BottomAnchor.ConstraintEqualTo(_rootsStackView.BottomAnchor, Dimensions.CommonBottomMargin).Active = true;
				RootView.RightAnchor.ConstraintEqualTo(_rootsStackView.RightAnchor, Dimensions.CommonRightMargin).Active = true;

				_shouldSetupConstraint = false;
			}

			base.UpdateConstraints();
		}
	}
}