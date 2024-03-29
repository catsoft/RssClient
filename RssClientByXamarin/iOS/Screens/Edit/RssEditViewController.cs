﻿using Autofac;
using Core;
using Core.Database.Rss;
using Core.Repositories.Rss;
using iOS.CustomUI;
using iOS.CustomUI.StyledView;
using iOS.Screens.Base.ViewController;
using iOS.Styles;
using UIKit;

namespace iOS.Screens.Edit
{
	public class RssEditViewController : BaseViewController
	{
		private readonly RssModel _item;
		private readonly IRssRepository _rssRepository;

		private WrappedStackView _stackView;
		private RoundTextField _urlField;
		private UIButton _submitButton;

		public RssEditViewController(RssModel item)
		{
			_item = item;
			_rssRepository = App.Container.Resolve<IRssRepository>();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (NavigationItem != null)
			{
				NavigationItem.Title = Strings.RssEditTitle;
			}

			_stackView = new WrappedStackView(View);

			InitUrlField();

			InitSubmitButton();

			_urlField.BecomeFirstResponder();
		}

		private void InitSubmitButton()
		{
			_submitButton = UIButton.FromType(UIButtonType.System);
			_submitButton.SetTitle("Edit", UIControlState.Normal);
			_submitButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_submitButton.AddGestureRecognizer(new UITapGestureRecognizer(async () =>
			{
				var url = _urlField.Text;
                var id = _item.Id;

				await _rssRepository.Update(id, url);

				NavigationController?.PopViewController(true);
			}));

			_stackView.AddArrangedSubview(_submitButton);
		}

		private void InitUrlField()
		{
            _urlField = new RoundTextField {Text = _item.Rss};

            _stackView.AddArrangedSubview(_urlField);
		}
	}
}