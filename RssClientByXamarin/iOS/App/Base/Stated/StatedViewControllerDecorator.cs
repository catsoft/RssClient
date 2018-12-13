﻿using UIKit;

namespace iOS.App.Base.Stated
{
	public class StatedViewControllerDecorator : UIViewControllerDecorator
	{
		private readonly UIViewController _controller;
		public ScreenState State { get; private set; }

		private UIView _loadView;
		private UIView _errorView;

		public StatedViewControllerDecorator(UIViewController controller) : base(controller)
		{
			_controller = controller;
		}

		public void SetNormal(NormalData data)
		{
			Clear();
			State = ScreenState.Normal;
		}

		public void SetLoading(LoadingData data)
		{
			Clear();
			_loadView = new LoadingView(Controller.View);
			_controller.View.AddSubview(_loadView);
			State = ScreenState.Loading;
		}

		public void SetError(ErrorData data)
		{
			Clear();
			_errorView = new ErrorView(Controller.View);
			_controller.View.AddSubview(_errorView);
			State = ScreenState.Error;
		}

		private void Clear()
		{
			_loadView?.RemoveFromSuperview();
			_errorView?.RemoveFromSuperview();
		}
	}
}