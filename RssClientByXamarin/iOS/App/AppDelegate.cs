using CoreGraphics;
using Foundation;
using iOS.App.Rss.List;
using iOS.App.Styles;
using Shared.AppCenter;
using UIKit;

namespace iOS.App
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
	    public new static UIWindow Window { get; private set; }
		public static AppDelegate Instance { get; private set; }

	    public UINavigationController NavigationController { get; private set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
	        Instance = this;

			InitNavigation();

            return true;
        }

	    private void InitNavigation()
	    {
			AppCenterContainer.Init();

		    UINavigationBar.Appearance.BarTintColor = Colors.PrimaryColor;
		    UINavigationBar.Appearance.BarStyle = UIBarStyle.Black;
		    UINavigationBar.Appearance.Translucent = false;

		    NavigationController = new UINavigationController();

		    NavigationController.NavigationBar.Layer.ShadowColor = Colors.Shadow.CGColor;
		    NavigationController.NavigationBar.Layer.ShadowRadius = 4;
		    NavigationController.NavigationBar.Layer.ShadowOpacity = 1;
		    NavigationController.NavigationBar.Layer.ShadowOffset = new CGSize(0, 0);

			Window = new UIWindow(UIScreen.MainScreen.Bounds)
		    {
			    RootViewController = NavigationController
		    };

			NavigationController.PushViewController(new RssListViewController(), true);

		    Window.MakeKeyAndVisible();
		}
    }
}

