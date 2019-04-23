using Autofac;
using Core;
using Core.Repositories.Rss;
using iOS.CustomUI;
using iOS.CustomUI.StyledView;
using iOS.Screens.Base.ViewController;
using iOS.Styles;
using UIKit;

namespace iOS.Screens.Create
{
	public class RssCreateViewController : BaseViewController
	{
		private readonly IRssRepository _rssRepository;
		private RoundTextField _urlField;
		private WrappedStackView _stackView;
		private UIButton _submitButton;

		public RssCreateViewController()
        {
            _rssRepository = App.Container.Resolve<IRssRepository>();
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssCreateTitle;
			}

			_stackView = new WrappedStackView(View);

			InitUrlField();

			InitSubmitButton();

			_urlField.BecomeFirstResponder();
		}

		private void InitSubmitButton()
		{
			_submitButton = UIButton.FromType(UIButtonType.System);
			_submitButton.SetTitle("Create", UIControlState.Normal);
			_submitButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_submitButton.AddGestureRecognizer(new UITapGestureRecognizer(async () =>
			{
				var text = _urlField.Text;
				await _rssRepository.AddAsync(text);

				NavigationController?.PopViewController(true);
			}));

			_stackView.AddArrangedSubview(_submitButton);
		}

		private void InitUrlField()
		{
            _urlField = new RoundTextField {Placeholder = "Url", Text = "http://"};

            _stackView.AddArrangedSubview(_urlField);
		}
	}
}