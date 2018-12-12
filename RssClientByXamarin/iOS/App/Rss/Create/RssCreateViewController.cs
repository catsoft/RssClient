using Database.Rss;
using iOS.App.Base.Table;
using iOS.App.CustomUI;
using iOS.App.Rss.List;
using iOS.App.Styles;
using Shared.App.Rss;
using UIKit;

namespace iOS.App.Rss.Create
{
	public class RssCreateViewController : BaseTableViewController<RssViewCell, RssModel>
	{
		private readonly RssRepository _rssRepository;
		private RoundTextField _urlField;
		private UIStackView _stackView;
		private UIButton _submitButton;

		public RssCreateViewController()
		{
			_rssRepository = RssRepository.Instance;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssCreateTitle;
			}

			InitStackView();

			InitUrlField();

			InitSubmitButton();
		}

		private void InitSubmitButton()
		{
			_submitButton = UIButton.FromType(UIButtonType.System);
			_submitButton.SetTitle("Create", UIControlState.Normal);
			_submitButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_submitButton.AddGestureRecognizer(new UITapGestureRecognizer(async () =>
			{
				var text = _urlField.Field.Text;
				await _rssRepository.Insert(text);

				NavigationController?.PopViewController(true);
			}));

			_stackView.AddArrangedSubview(_submitButton);
		}

		private void InitStackView()
		{
			_stackView = new UIStackView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Axis = UILayoutConstraintAxis.Vertical,
				Spacing = 10,
			};

			View.AddSubview(_stackView);

			_stackView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
			_stackView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
			_stackView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
		}

		private void InitUrlField()
		{
			_urlField = new RoundTextField();
			_urlField.Field.Placeholder = "Url";
			_urlField.Field.Text = "http://";

			_stackView.AddArrangedSubview(_urlField);
		}
	}
}