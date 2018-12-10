using UIKit;

namespace iOS.App.Base.Stated
{
	public class StatedViewControllerDecorator : UIViewControllerDecorator
	{
		public ScreenState State { get; private set; }

		private UIView _loadView;
		private UIView _errorView;

		public StatedViewControllerDecorator(UIViewController controller) : base(controller)
		{
		}

		public void SetNormal(NormalData data)
		{
			_loadView?.RemoveFromSuperview();
			_errorView?.RemoveFromSuperview();
			State = ScreenState.Normal;
		}

		public void SetLoading(LoadingData data)
		{
			Clear();
			_loadView = new LoadingView(Controller.View);
			State = ScreenState.Loading;
		}

		public void SetError(ErrorData data)
		{
			Clear();
			_errorView = new ErrorView(Controller.View);
			State = ScreenState.Error;
		}

		private void Clear()
		{
			_errorView?.RemoveFromSuperview();
		}
	}
}