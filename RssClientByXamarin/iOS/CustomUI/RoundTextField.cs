using iOS.Styles;
using UIKit;
using Xamarin;

namespace iOS.CustomUI
{
	public sealed class RoundTextField : IQTextView
	{
		public RoundTextField()
		{
			TranslatesAutoresizingMaskIntoConstraints = false;
			TextAlignment = UITextAlignment.Center;
			TextContainer.MaximumNumberOfLines = 1;
			TextContainer.LineBreakMode = UILineBreakMode.HeadTruncation;
			Font = UIFont.SystemFontOfSize(16);

			HeightAnchor.ConstraintEqualTo(36).Active = true;

			Layer.CornerRadius = 10;
			Layer.BorderWidth = 1;
			Layer.BorderColor = Colors.PrimaryColor.CGColor;
		}
	}
}