using UIKit;

namespace iOS.Screens.Base.Stated
{
	public class UIViewControllerDecorator
	{
		protected UIViewController Controller { get; }

		public UIViewControllerDecorator(UIViewController controller)
		{
			Controller = controller;
		}
	}
}