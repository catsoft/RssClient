using iOS.App.Styles;
using UIKit;
using Xamarin;

namespace iOS.App.CustomUI
{
	public sealed class RoundTextField : UIView
	{
		private const float Offset = 5;

		public IQTextView Field { get; }

		public RoundTextField()
		{
			Field = new UITextField()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			TranslatesAutoresizingMaskIntoConstraints = false;

			AddSubview(Field);

			LeadingAnchor.ConstraintEqualTo(Field.LeadingAnchor, Offset).Active = true;
			TrailingAnchor.ConstraintEqualTo(Field.TrailingAnchor, Offset).Active = true;
			TopAnchor.ConstraintEqualTo(Field.TopAnchor, Offset).Active = true;
			BottomAnchor.ConstraintEqualTo(Field.BottomAnchor, Offset).Active = true;

			Layer.CornerRadius = 5;
			Layer.BorderWidth = 2;
			Layer.BorderColor = Colors.PrimaryColor.CGColor;
		}
	}
}