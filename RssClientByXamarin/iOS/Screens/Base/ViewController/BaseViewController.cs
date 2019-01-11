using Autofac;
using iOS.Screens.Base.Stated;
using iOS.Styles;
using Shared;
using Shared.Analytics.Rss;
using UIKit;

namespace iOS.Screens.Base.ViewController
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

            App.Container.Resolve<ScreenLog>().TrackScreenOpen(GetType());
		}
	}
}