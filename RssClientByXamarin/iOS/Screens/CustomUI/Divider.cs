using iOS.Screens.Styles;
using UIKit;

namespace iOS.Screens.CustomUI
{
	public sealed class Divider : UIView
	{
		public Divider()
		{
			TranslatesAutoresizingMaskIntoConstraints = false;
			HeightAnchor.ConstraintEqualTo(1).Active = true;
			BackgroundColor = Colors.Divider;
		}
	}
}