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
        private Guid _itemId;
        [NotNull] private RssFeedEditFragmentViewHolder _viewHolder;

        // ReSharper disable once NotNullMemberIsNotInitialized
        public RssFeedEditFragment() { }

        // ReSharper disable once NotNullMemberIsNotInitialized
        public RssFeedEditFragment(Guid itemId) { _itemId = itemId; }

        protected override int LayoutId => Resource.Layout.fragment_rss_edit;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState([NotNull] Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_itemId), _itemId.ToString());
        }

        protected override void RestoreState([NotNull] Bundle saved) { _itemId = Guid.Parse(saved.GetString(nameof(_itemId)).NotNull()); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

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
                        if (action.ActionId == ImeAction.Done) _viewHolder.EditText.CallOnClick();
                    })
                    .AddTo(disposable);

                this.BindCommand(ViewModel, model => model.UpdateCommand, fragment => fragment._viewHolder.SendButton)
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(w => w.Url)
                    .Select(w => !Patterns.WebUrl.Matcher(_viewHolder.EditText.Text).Matches())
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
