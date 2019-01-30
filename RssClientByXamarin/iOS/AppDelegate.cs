using Autofac;
using Foundation;
using iOS.Container.Modules;
using iOS.Screens.List;
using iOS.Styles;
using Shared;
using Shared.Analytics;
using UIKit;

namespace iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private const string KeyBattleIos = "9e54385d-f3a3-492d-a350-f538d59d742b";
        private const string KeyDebugIos = "a7eee657-65a4-4fa8-a318-1b367855334b";

        public new static UIWindow Window { get; private set; }
		public static AppDelegate Instance { get; private set; }

	    public UINavigationController NavigationController { get; private set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
	        Instance = this;

            App.Build(new PlatformModule());

			InitNavigation();

            return true;
        }

	    private void InitNavigation()
        {
            var log = App.Container.Resolve<ILog>();

#if DEBUG
            {
                log.SetApiKey(KeyDebugIos);
            }
#else
{
                log.SetApiKey(ApiKeyBattleIos);
}
#endif

		    UINavigationBar.Appearance.BarTintColor = Colors.PrimaryColor;
		    UINavigationBar.Appearance.BarStyle = UIBarStyle.Black;
		    UINavigationBar.Appearance.Translucent = false;
		    UINavigationBar.Appearance.TintColor = Colors.SecondaryColor;

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

