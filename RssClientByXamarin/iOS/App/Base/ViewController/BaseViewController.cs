using iOS.App.Base.Stated;
using iOS.App.Styles;
using UIKit;

namespace iOS.App.Base.ViewController
{
	public class BaseViewController : UIViewController
	{
		public StatedViewControllerDecorator StatedDecorator { get; }
		public BaseViewController()
		{
			StatedDecorator = new StatedViewControllerDecorator(this);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = Colors.CommonBack;
		}
	}
}