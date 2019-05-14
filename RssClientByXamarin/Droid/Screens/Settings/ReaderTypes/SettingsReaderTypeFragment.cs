using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Core.ViewModels.Settings.ReaderTypes;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;
using Core.Configuration.Settings;

namespace Droid.Screens.Settings.ReaderTypes
{
    public class SettingsReaderTypeFragment : BaseFragment<SettingsReaderTypeViewModel>
    {
        [NotNull] private SettingsReaderTypeFragmentViewHolder _viewHolder;
        
        protected override int LayoutId => Resource.Layout.fragment_settings_reader_type;
        
        public override bool IsRoot => false;
        
        protected override void RestoreState(Bundle saved)
        {
        }

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public SettingsReaderTypeFragment()
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _viewHolder = new SettingsReaderTypeFragmentViewHolder(view);
            
            OnActivation(disposable =>
            {
                _viewHolder.MainRadioGroup.Events()
                    .NotNull()
                    .CheckedChange
                    .NotNull()
                    .Select(w => w.NotNull().CheckedId)
                    .Select(ConvertToReaderType)
                    .InvokeCommand(ViewModel.UpdateReaderTypeCommand)
                    .AddTo(disposable);
                
                ViewModel.AppConfigurationViewModel.WhenAnyValue(w => w.AppConfiguration)
                    .NotNull()
                    .Select(w => w.NotNull().ReaderType)
                    .Select(ConvertToId)
                    .Subscribe(w => _viewHolder.MainRadioGroup.Check(w))
                    .AddTo(disposable);
            });
            
            return view;
        }
        
        private ReaderType ConvertToReaderType(int id)
        {
            if (id == _viewHolder.StripRadioButton.Id)
                return ReaderType.Strip;
            return id == _viewHolder.BookRadioButton.Id ? ReaderType.Book : ReaderType.Strip;
        }

        private int ConvertToId(ReaderType messagesViewer)
        {
            switch (messagesViewer)
            {
                default:
                    return _viewHolder.StripRadioButton.Id;
                case ReaderType.Book:
                    return _viewHolder.BookRadioButton.Id;
            }
        }
    }
}