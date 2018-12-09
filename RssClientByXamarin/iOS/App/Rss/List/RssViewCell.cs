using iOS.App.Base.Table;
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
			_rootsStackView = new UIStackView();

			_nameLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_urlLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			ContentView.AddSubview(_rootsStackView);
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

				_shouldSetupConstraint = false;
			}

			base.UpdateConstraints();
		}
	}
}