using Analytics.Rss;
using Autofac;
using iOS.Screens.Base.Stated;
using iOS.Screens.Styles;
using Shared;
using UIKit;

namespace iOS.Screens.Base.ViewController
{
	public class BaseViewController : UIViewController
	{
        private ScreenLog _screenLog;

        public StatedViewControllerDecorator StatedDecorator { get; }
		public BaseViewController()
		{
			StatedDecorator = new StatedViewControllerDecorator(this);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            _screenLog = App.Container.Resolve<ScreenLog>();

            View.BackgroundColor = Colors.CommonBack;

            _screenLog.TrackScreenOpen(GetType());
		}
	}
}