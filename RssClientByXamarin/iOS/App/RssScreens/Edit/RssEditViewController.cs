using Database.Rss;
using iOS.App.Base.StyledView;
using iOS.App.Base.ViewController;
using iOS.App.CustomUI;
using iOS.App.Styles;
using Shared.App.Rss;
using UIKit;

namespace iOS.App.RssScreens.Edit
{
	public class RssEditViewController : BaseViewController
	{
		private readonly RssModel _item;
		private readonly RssRepository _rssRepository;

		private WrappedStackView _stackView;
		private RoundTextField _nameTextField;
		private RoundTextField _urlField;
		private UIButton _submitButton;

		public RssEditViewController(RssModel item)
		{
			_item = item;
			_rssRepository = RssRepository.Instance;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssEditTitle;
			}

			_stackView = new WrappedStackView(View);

			InitNameField();

			InitUrlField();

			InitSubmitButton();

			_nameTextField.BecomeFirstResponder();
		}

		private void InitNameField()
		{
			_nameTextField = new RoundTextField();
			_nameTextField.Text = _item.Name;

			_stackView.AddArrangedSubview(_nameTextField);
		}

		private void InitSubmitButton()
		{
			_submitButton = UIButton.FromType(UIButtonType.System);
			_submitButton.SetTitle("Edit", UIControlState.Normal);
			_submitButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_submitButton.AddGestureRecognizer(new UITapGestureRecognizer(async () =>
			{
				var name = _nameTextField.Text;
				var url = _urlField.Text;
                var id = _item.Id;

				await _rssRepository.Update(id, url, name);

				NavigationController?.PopViewController(true);
			}));

			_stackView.AddArrangedSubview(_submitButton);
		}

		private void InitUrlField()
		{
			_urlField = new RoundTextField();
			_urlField.Text = _item.Rss;

			_stackView.AddArrangedSubview(_urlField);
		}
	}
}