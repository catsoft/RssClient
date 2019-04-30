using System;
using Android.OS;
using Android.Views;
using Core.Extensions;
using Core.ViewModels.Messages.Message;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Messages.Message
{
    public class MessageFragment : BaseFragment<MessageViewModel>
    {
        [NotNull] private MessageFragmentViewHolder _viewHolder;
        
        private Guid _rssMessageId;
        
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once NotNullMemberIsNotInitialized
        public MessageFragment() { }

        // ReSharper disable once NotNullMemberIsNotInitialized
        public MessageFragment(Guid rssMessageId) { _rssMessageId = rssMessageId; }

        protected override int LayoutId => Resource.Layout.fragment_rss_message;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_rssMessageId), _rssMessageId.ToString());
        }

        protected override void RestoreState(Bundle saved) { _rssMessageId = Guid.Parse(saved.GetString(nameof(_rssMessageId))); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new MessageFragmentViewHolder(view);
            
            OnActivation(disposable =>
            {
                ViewModel.WhenAnyValue(model => model.Parameters)
                    .Subscribe(w => Title = w.RssMessageModel.Title)
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(model => model.Parameters)
                    .Subscribe(w => _viewHolder.WebView.LoadUrl(w.RssMessageModel.Url))
                    .AddTo(disposable);
            });

            return view;
        }
    }
}
