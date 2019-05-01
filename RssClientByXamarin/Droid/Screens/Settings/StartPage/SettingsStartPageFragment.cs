using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.ViewModels.Settings.StartPage;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Settings.StartPage
{
    public class SettingsStartPageFragment : BaseFragment<SettingsStartPageViewModel>
    {
        [NotNull] private SettingsStartPageFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_settings_start_page;

        public override bool IsRoot => false;

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public SettingsStartPageFragment() { }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new SettingsStartPageFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                _viewHolder.RadioGroup.Events()
                    .NotNull()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().CheckedId)
                    .Select(ConvertToStartPage)
                    .InvokeCommand(ViewModel.UpdateStartPageCommand)
                    .AddTo(disposable);

                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .NotNull()
                    .Select(w => w.NotNull().StartPage)
                    .Select(ConvertToInt)
                    .Subscribe(w => _viewHolder.RadioGroup.Check(w))
                    .AddTo(disposable);
            });

            return view;
        }

        private int ConvertToInt(Core.Configuration.Settings.StartPage startPage)
        {
            switch (startPage)
            {
                default:
                    return _viewHolder.RssListRadioButton.Id;
                case Core.Configuration.Settings.StartPage.AllMessages:
                    return _viewHolder.AllMessagesRadioButton.Id;
            }
        }

        private Core.Configuration.Settings.StartPage ConvertToStartPage(int id)
        {
            if (id == _viewHolder.RssListRadioButton.Id) return Core.Configuration.Settings.StartPage.RssList;

            return id == _viewHolder.AllMessagesRadioButton.Id
                ? Core.Configuration.Settings.StartPage.AllMessages
                : Core.Configuration.Settings.StartPage.RssList;
        }
    }
}
