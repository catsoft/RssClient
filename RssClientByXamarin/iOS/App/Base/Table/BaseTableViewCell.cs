using iOS.App.Base.StyledView;
using iOS.App.Styles;
using UIKit;

namespace iOS.App.Base.Table
{
	public abstract class BaseTableViewCell<TItem> : UITableViewCell
		where TItem : class 
	{
		private bool _shouldSetupConstraint = true;

		protected RoundShadowView RootView { get; }

		protected BaseTableViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
			RootView = new RoundShadowView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			SelectionStyle = UITableViewCellSelectionStyle.None;
			ContentView.BackgroundColor = Colors.CommonBack;

			ContentView.AddSubview(RootView);
		}

		public override void UpdateConstraints()
		{
			base.UpdateConstraints();

			if (_shouldSetupConstraint)
			{
				ContentView.TopAnchor.ConstraintEqualTo(RootView.TopAnchor, -Dimensions.CommonTopMargin).Active = true;
				ContentView.LeadingAnchor.ConstraintEqualTo(RootView.LeadingAnchor, -Dimensions.CommonLeftMargin).Active = true;
				ContentView.BottomAnchor.ConstraintEqualTo(RootView.BottomAnchor, Dimensions.CommonBottomMargin).Active = true;
				ContentView.TrailingAnchor.ConstraintEqualTo(RootView.TrailingAnchor, Dimensions.CommonRightMargin).Active = true;

				_shouldSetupConstraint = false;
			}
		}

		public abstract void BindData(TItem item);
	}
}