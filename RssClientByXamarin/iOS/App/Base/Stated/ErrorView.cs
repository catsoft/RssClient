using UIKit;

namespace iOS.App.Base.Stated
{
	public class ErrorView : UIView
    {
        private UILabel _errorLabel;

		public ErrorView(UIView parentView)
        {
            var frame = parentView.Frame;
            frame.X = 0;
            frame.Y = 0;
            Frame = frame;

            InitErrorLabel();
        }

        private void InitErrorLabel()
        {
            _errorLabel = new UILabel()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            AddSubview(_errorLabel);

            _errorLabel.CenterXAnchor.ConstraintEqualTo(CenterXAnchor).Active = true;
            _errorLabel.CenterYAnchor.ConstraintEqualTo(CenterYAnchor).Active = true;
        }

        public void BindData(ErrorData data)
        {
            _errorLabel.Text = data.Message;
        }
	}
}