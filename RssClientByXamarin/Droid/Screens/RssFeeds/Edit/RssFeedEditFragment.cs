using System;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Core.Extensions;
using Core.ViewModels.RssFeeds.Edit;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Resources;
using Droid.Screens.Navigation;
using ReactiveUI;

namespace Droid.Screens.RssFeeds.Edit
{
    public class RssFeedEditFragment : BaseFragment<RssFeedEditViewModel>
    {
        private Guid  _itemId;
        private RssFeedEditFragmentViewHolder _viewHolder;

        public RssFeedEditFragment() { }

        public RssFeedEditFragment(Guid itemId) { _itemId = itemId; }

        protected override int LayoutId => Resource.Layout.fragment_rss_edit;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_itemId), _itemId.ToString());
        }

        protected override void RestoreState(Bundle saved) { _itemId = Guid.Parse(saved.GetString(nameof(_itemId))); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.edit_title);

            _viewHolder = new RssFeedEditFragmentViewHolder(view);

            OnActivation(compositeDisposable =>
            {
                this.Bind(ViewModel, model => model.Url, fragment => fragment._viewHolder.EditText.Text)
                    .AddTo(compositeDisposable);

                ViewModel.Url.WhenAnyValue(s => s)
                    .Subscribe(s => _viewHolder.TextInputLayout.EditText.SetTextAndSetCursorToLast(s))
                    .AddTo(compositeDisposable);

                _viewHolder.EditText.GetEditorAction()
                    .Subscribe(action =>
                    {
                        if (action.ActionId == ImeAction.Done) _viewHolder.EditText.CallOnClick();
                    })
                    .AddTo(compositeDisposable);

                this.BindCommand(ViewModel, model => model.UpdateCommand, fragment => fragment._viewHolder.SendButton)
                    .AddTo(compositeDisposable);

                ViewModel.LoadCommand.Execute().Subscribe();
            });

            return view;
        }
    }
}
