using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Configuration.Settings;
using Core.Extensions;
using Core.ViewModels.Settings.RssDetail;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.RssDetail
{
    public class SettingsRssDetailFragment : BaseFragment<SettingsRssDetailViewModel>
    {
        [NotNull] private SettingsRssDetailFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_rss_detail;

        public override bool IsRoot => false;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public SettingsRssDetailFragment()
        {
        }
        
        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsRssDetailFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                _viewHolder.RadioGroup.Events()
                    .NotNull()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().CheckedId)
                    .Select(ConvertToViewer)
                    .InvokeCommand(ViewModel.UpdateRssDetailCommand)
                    .AddTo(disposable);
                
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .NotNull()
                    .Select(w => w.NotNull().MessagesViewer)
                    .Select(ConvertToId)
                    .Subscribe(w => _viewHolder.RadioGroup.Check(w))
                    .AddTo(disposable);
            });
            
            return view;
        }

        private MessagesViewer ConvertToViewer(int id)
        {
            if (id == _viewHolder.InAppRadioButton.Id)
                return MessagesViewer.App;
            return id == _viewHolder.InBrowserRadioButton.Id ? MessagesViewer.Browser : MessagesViewer.App;
        }

        private int ConvertToId(MessagesViewer messagesViewer)
        {
            switch (messagesViewer)
            {
                default:
                    return _viewHolder.InAppRadioButton.Id;
                case MessagesViewer.Browser:
                    return _viewHolder.InBrowserRadioButton.Id;
            }
        }
    }
}
