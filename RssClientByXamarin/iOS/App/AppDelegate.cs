using Foundation;
using iOS.App.Rss.List;
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
		    NavigationController = new UINavigationController();
		    Window = new UIWindow(UIScreen.MainScreen.Bounds)
		    {
			    RootViewController = NavigationController
		    };

			NavigationController.PushViewController(new RssListViewController(), true);

		    Window.MakeKeyAndVisible();
		}
    }
}

