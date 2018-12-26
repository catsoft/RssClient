using UIKit;

namespace iOS.App.Base.Stated
{
	public class LoadingView : UIView
	{
		public LoadingView(UIView parentView)
		{
            var frame = parentView.Frame;
            frame.X = 0;
            frame.Y = 0;
            Frame = frame;

            BackgroundColor = UIColor.Red;
		}
	}
}