using iOS.Screens.CustomUI;
using iOS.Screens.Styles;
using UIKit;

namespace iOS.Screens.Base.Table
{
	public abstract class BaseTableViewCell<TItem> : UITableViewCell
		where TItem : class 
	{
		private bool _shouldSetupConstraint = true;

		protected UIView RootView { get; }
		private readonly Divider _divider;

		protected BaseTableViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
			RootView = new UIView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_divider = new Divider();

			SelectionStyle = UITableViewCellSelectionStyle.None;
			ContentView.BackgroundColor = Colors.ListItemBack;

			ContentView.AddSubview(RootView);
			ContentView.AddSubview(_divider);
		}

		public override void UpdateConstraints()
		{
			base.UpdateConstraints();

			if (_shouldSetupConstraint)
			{
				RootView.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, Dimensions.CommonTopMargin).Active = true;
				RootView.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, Dimensions.CommonLeftMargin).Active = true;
				RootView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -Dimensions.CommonRightMargin).Active = true;

				_divider.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
				_divider.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor).Active = true;
				_divider.TopAnchor.ConstraintEqualTo(RootView.BottomAnchor, Dimensions.CommonBottomMargin).Active = true;
				_divider.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;

				_shouldSetupConstraint = false;
			}
		}

		public abstract void BindData(TItem item);
	}
}