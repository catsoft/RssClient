using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Core.Extensions;
using Core.ViewModels.RssFeeds.Edit;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.RssFeeds.Edit
{
    public class RssFeedEditFragment : BaseFragment<RssFeedEditViewModel>
    {
        [NotNull] private RssFeedEditFragmentViewHolder _viewHolder;

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public RssFeedEditFragment() { }

        protected override int LayoutId => Resource.Layout.fragment_rss_edit;
        public override bool IsRoot => false;

        protected override void RestoreState([NotNull] Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            Title = GetText(Resource.String.edit_title);

            _viewHolder = new RssFeedEditFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                this.Bind(ViewModel, model => model.Url, fragment => fragment._viewHolder.EditText.Text)
                    .AddTo(disposable);

                ViewModel.Url.WhenAnyValue(s => s)
                    .NotNull()
                    .Subscribe(s => _viewHolder.TextInputLayout.EditText.SetTextAndSetCursorToLast(s))
                    .AddTo(disposable);

                _viewHolder.EditText.GetEditorAction()
                    .Subscribe(action =>
                    {
                        if (action?.ActionId == ImeAction.Done) _viewHolder.EditText.CallOnClick();
                    })
                    .AddTo(disposable);

                this.BindCommand(ViewModel, model => model.UpdateCommand, fragment => fragment._viewHolder.SendButton)
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(w => w.Url)
                    .NotNull()
                    .Select(w => !Patterns.WebUrl.NotNull().Matcher(_viewHolder.EditText.Text).NotNull().Matches())
                    .Subscribe(w => ViewModel.IsUrlInvalid = w)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(w => w.ErrorMessage)
                    .BindTo(_viewHolder.TextInputLayout, layout => layout.Error)
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(w => w.IsUrlInvalid)
                    .BindTo(_viewHolder.TextInputLayout, layout => layout.ErrorEnabled)
                    .AddTo(disposable);
                
                ViewModel.LoadCommand.ExecuteIfCan();
            });

            return view;
        }
    }
}
