using UIKit;

namespace iOS.App.Base.Table
{
	public abstract class BaseTableViewCell<TItem> : UITableViewCell
		where TItem : class 
	{
		private bool _shouldSetupConstraint = true;

		protected UIView RootView { get; private set; }

		protected BaseTableViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
			RootView = new UIView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			ContentView.AddSubview(RootView);
		}

		public override void UpdateConstraints()
		{
			base.UpdateConstraints();

			if (_shouldSetupConstraint)
			{
				ContentView.TopAnchor.ConstraintEqualTo(RootView.TopAnchor).Active = true;
				ContentView.LeadingAnchor.ConstraintEqualTo(RootView.LeadingAnchor).Active = true;
				ContentView.BottomAnchor.ConstraintEqualTo(RootView.BottomAnchor).Active = true;
				ContentView.TrailingAnchor.ConstraintEqualTo(RootView.TrailingAnchor).Active = true;

				_shouldSetupConstraint = false;
			}
		}

		public abstract void BindData(TItem item);
	}
}