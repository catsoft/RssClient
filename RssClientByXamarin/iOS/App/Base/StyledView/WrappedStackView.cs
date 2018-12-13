using CoreGraphics;
using iOS.App.Styles;
using UIKit;

namespace iOS.App.Base.StyledView
{
	public sealed class WrappedStackView : UIStackView
	{
		private const float InnerOffset = 20;
		private const float OutOffset = 30;

		public UIView Wrapper { get; private set; }

		public WrappedStackView(UIView parentView)
		{
			InitWrapper(parentView);

			TranslatesAutoresizingMaskIntoConstraints = false;
			Axis = UILayoutConstraintAxis.Vertical;
			Spacing = 15;
			Alignment = UIStackViewAlignment.Fill;

			Wrapper.AddSubview(this);

			TrailingAnchor.ConstraintEqualTo(Wrapper.TrailingAnchor, -InnerOffset).Active = true;
			LeadingAnchor.ConstraintEqualTo(Wrapper.LeadingAnchor, InnerOffset).Active = true;
			TopAnchor.ConstraintEqualTo(Wrapper.TopAnchor, InnerOffset).Active = true;
			BottomAnchor.ConstraintEqualTo(Wrapper.BottomAnchor, -InnerOffset).Active = true;
		}

		private void InitWrapper(UIView parentView)
		{
			Wrapper = new UIView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = Colors.ListItemBack,
			};
			Wrapper.Layer.CornerRadius = 10;
			Wrapper.Layer.ShadowColor = Colors.Shadow.CGColor;
			Wrapper.Layer.ShadowRadius = 10;
			Wrapper.Layer.ShadowOpacity = 1;
			Wrapper.Layer.ShadowOffset = new CGSize(0, 0);

			parentView.AddSubview(Wrapper);

			Wrapper.TrailingAnchor.ConstraintEqualTo(parentView.TrailingAnchor, -OutOffset).Active = true;
			Wrapper.LeadingAnchor.ConstraintEqualTo(parentView.LeadingAnchor, OutOffset).Active = true;
			Wrapper.TopAnchor.ConstraintEqualTo(parentView.TopAnchor, OutOffset).Active = true;
		}
	}
}