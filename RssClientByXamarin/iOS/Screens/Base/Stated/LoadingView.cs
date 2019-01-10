using UIKit;

namespace iOS.Screens.Base.Stated
{
	public class LoadingView : UIView
	{
		public LoadingView(UIView parentView)
		{
            var frame = parentView.Frame;
            frame.X = 0;
            frame.Y = 0;
            Frame = frame;
		}

        public void BindData(LoadingData data)
        {
            
        }
	}
}