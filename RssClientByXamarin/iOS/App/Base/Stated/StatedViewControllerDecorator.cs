using UIKit;

namespace iOS.App.Base.Stated
{
	public class StatedViewControllerDecorator
	{
		public ScreenState State { get; private set; }

		private UIViewController _controller;

		private UIView _loadView;
		private UIView _errorView;

		public StatedViewControllerDecorator(UIViewController controller)
		{
			_controller = controller;
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
			_loadView = new LoadingView(_controller.View);
			State = ScreenState.Loading;
		}

		public void SetError(ErrorData data)
		{
			Clear();
			_errorView = new ErrorView(_controller.View);
			State = ScreenState.Error;
		}

		private void Clear()
		{
			_errorView?.RemoveFromSuperview();
		}
	}
}