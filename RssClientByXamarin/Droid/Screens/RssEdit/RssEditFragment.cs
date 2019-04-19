#region

using System;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Droid.NativeExtension;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Extensions;
using Shared.ViewModels.RssEdit;

#endregion

namespace Droid.Screens.RssEdit
{
    public class RssEditFragment : BaseFragment<RssEditViewModel>
    {
        private string _itemId;
        private RssEditFragmentViewHolder _viewHolder;

        public RssEditFragment() { }

        public RssEditFragment(string itemId) { _itemId = itemId; }

        protected override int LayoutId => Resource.Layout.fragment_rss_edit;
        public override bool IsRoot => false;

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_itemId), _itemId);
        }

        protected override void RestoreState(Bundle saved) { _itemId = saved.GetString(nameof(_itemId)); }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.edit_title);

            _viewHolder = new RssEditFragmentViewHolder(view);

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
