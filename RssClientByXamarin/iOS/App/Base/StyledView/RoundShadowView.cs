using CoreAnimation;
using CoreGraphics;
using iOS.App.Styles;
using UIKit;

namespace iOS.App.Base.StyledView
{
	public class RoundShadowView : UIView
	{
		public RoundShadowView()
		{
			Layer.CornerRadius = Dimensions.CornerRadius;
			Layer.ShadowOffset = new CGSize(5, 5);
			Layer.ShadowOpacity = 0.6f;
			Layer.ShadowColor = Colors.Shadow.CGColor;
			Layer.ShadowRadius = 4;
			Layer.MaskedCorners = CACornerMask.MaxXMinYCorner | CACornerMask.MinXMaxYCorner | CACornerMask.MinXMinYCorner;

			BackgroundColor = Colors.ListItemBack;
		}
	}
}