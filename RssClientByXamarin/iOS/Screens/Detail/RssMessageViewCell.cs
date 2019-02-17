using System;
using Autofac;
using Foundation;
using iOS.Screens.Base.Table;
using iOS.Styles;
using Plugin.Share;
using Plugin.Share.Abstractions;
using SDWebImage;
using Shared;
using Shared.Analytics.Rss;
using Shared.Database.Rss;
using Shared.Repository;
using Shared.Services.Locale;
using UIKit;

namespace iOS.Screens.Detail
{
	public class RssMessageViewCell : BaseTableViewCell<RssMessageModel>
	{
		private readonly IRssMessagesRepository _rssMessagesesRepository;
        private RssMessageLog _log;
		private bool _shouldSetupConstraint = true;
		private readonly UIStackView _rootStackView;

		private readonly UILabel _dateLabel;
		private readonly UILabel _titleLabel;
		private readonly UILabel _contentLabel;
		private readonly UIImageView _imageContentView;

		private readonly UIStackView _actionsStackView;
		private readonly UIButton _readAction;
		private readonly UIButton _deleteAction;
		private readonly UIButton _markReadAction;
		private readonly UIButton _shareAction;
		private RssMessageModel _item;

		public event Action<RssMessageModel> ReadClick;
		public event Action<RssMessageModel> DeleteClick;
		public event Action<RssMessageModel> MarkAsReadClick;
		public event Action<RssMessageModel> ShareClick;

		public RssMessageViewCell(UITableViewCellStyle @default, string cellIdentifier) : base(@default, cellIdentifier)
		{
			_rssMessagesesRepository = App.Container.Resolve<IRssMessagesRepository>();
            _log = App.Container.Resolve<RssMessageLog>();

            _rootStackView = new UIStackView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Axis = UILayoutConstraintAxis.Vertical,
				Spacing = 5
			};

			_dateLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Font = UIFont.SystemFontOfSize(16)
			};

			_titleLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Lines = 0,
				Font = UIFont.FromName("Helvetica-bold", 18),
			};

			_contentLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Lines = 0,
				Font = UIFont.SystemFontOfSize(14),
			};

			_imageContentView = new UIImageView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ContentMode = UIViewContentMode.ScaleAspectFit,
			};

			_actionsStackView = new UIStackView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Axis = UILayoutConstraintAxis.Horizontal,
				Spacing = 10,
				Distribution = UIStackViewDistribution.FillProportionally,
			};

			_readAction = UIButton.FromType(UIButtonType.System);
			_readAction.SetTitle("Read more", UIControlState.Normal);
			_readAction.TranslatesAutoresizingMaskIntoConstraints = false;
			_readAction.AddGestureRecognizer(new UITapGestureRecognizer(() => ReadClick?.Invoke(_item)));

			_deleteAction = UIButton.FromType(UIButtonType.System);
			_deleteAction.SetTitle("Delete", UIControlState.Normal);
			_deleteAction.TranslatesAutoresizingMaskIntoConstraints = false;
			_deleteAction.AddGestureRecognizer(new UITapGestureRecognizer(() => DeleteClick?.Invoke(_item)));

			_markReadAction = UIButton.FromType(UIButtonType.System);
			_markReadAction.SetTitle("Mark as read", UIControlState.Normal);
			_markReadAction.TranslatesAutoresizingMaskIntoConstraints = false;
			_markReadAction.AddGestureRecognizer(new UITapGestureRecognizer(() => MarkAsReadClick?.Invoke(_item)));

			_shareAction = UIButton.FromType(UIButtonType.System);
			_shareAction.SetTitle("Share", UIControlState.Normal);
			_shareAction.TranslatesAutoresizingMaskIntoConstraints = false;
			_shareAction.AddGestureRecognizer(new UITapGestureRecognizer(() => ShareClick?.Invoke(_item)));

			RootView.AddSubview(_rootStackView);

			_rootStackView.AddArrangedSubview(_dateLabel);
			_rootStackView.AddArrangedSubview(_titleLabel);
			_rootStackView.AddArrangedSubview(_contentLabel);
			_rootStackView.AddArrangedSubview(_imageContentView);
			_rootStackView.AddArrangedSubview(_actionsStackView);

			_actionsStackView.AddArrangedSubview(_readAction);
			_actionsStackView.AddArrangedSubview(_deleteAction);
			_actionsStackView.AddArrangedSubview(_markReadAction);
			_actionsStackView.AddArrangedSubview(_shareAction);

			InitAction();
		}

		private void InitAction()
		{
			// TODO воскресить?
//			DeleteClick += (model) =>
//			{
//                _log.TrackMessageDelete(_item.RssLink, _item.SyndicationId, _item.Title);
//
//                _rssMessagesesRepository.MarkAsDeleted(model);
//			};

			MarkAsReadClick += (model) =>
			{
                _log.TrackMessageMarkAsRead(_item.RssLink, _item.SyndicationId, _item.Title);

                _rssMessagesesRepository.MarkAsRead(model);
            };

			ReadClick += (model) =>
			{
                _log.TrackMessageReadMore(_item.RssLink, _item.SyndicationId, _item.Title);

                _rssMessagesesRepository.MarkAsRead(model);

                // Совсем не очевидное название но открывает url (Если может)
                CrossShare.Current.CanOpenUrl(model.Url ?? "");
			};

			ShareClick += async model =>
			{
                _log.TrackMessageShare(_item.RssLink, _item.SyndicationId, _item.Title);

                var shareMessage = new ShareMessage()
				{
					Text = "RssParent link from RSS Client by \"Catsoft\"",
					Title = "Share RSS link",
					Url = model.Url ?? "",
				};
				await CrossShare.Current.Share(shareMessage).ConfigureAwait(false);
			};
		}

		public override void BindData(RssMessageModel item)
        {
            _item = item;

            _item.PropertyChanged += (sender, args) => PropertyChange();

            _dateLabel.Text = item.CreationDate.ToShortGeneralLocaleString();
			_titleLabel.Text = item.Title;
			_contentLabel.Text = item.Text;
			_imageContentView.SetImage(new NSUrl(item.ImageUrl ?? ""));

			_dateLabel.Enabled = !item.IsRead;
			_titleLabel.Enabled = !item.IsRead;
			_contentLabel.Enabled = !item.IsRead;
		}

        private void PropertyChange()
        {
            BindData(_item);
        }

		public override void UpdateConstraints()
		{
			base.UpdateConstraints();

			if (_shouldSetupConstraint)
			{
				RootView.TopAnchor.ConstraintEqualTo(_rootStackView.TopAnchor, -Dimensions.CommonTopMargin).Active = true;
				RootView.LeftAnchor.ConstraintEqualTo(_rootStackView.LeftAnchor, -Dimensions.CommonLeftMargin).Active = true;
				RootView.BottomAnchor.ConstraintEqualTo(_rootStackView.BottomAnchor, Dimensions.CommonBottomMargin).Active = true;
				RootView.RightAnchor.ConstraintEqualTo(_rootStackView.RightAnchor, Dimensions.CommonRightMargin).Active = true;

				_shouldSetupConstraint = false;
			}
		}
	}
}