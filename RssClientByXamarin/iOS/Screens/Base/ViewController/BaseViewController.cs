using Autofac;
using Core;
using Core.Analytics.Rss;
using iOS.Screens.Base.Stated;
using iOS.Styles;
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