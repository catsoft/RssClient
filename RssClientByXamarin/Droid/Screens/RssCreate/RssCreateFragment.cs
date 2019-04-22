using System;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Droid.NativeExtension;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Extensions;
using Shared.ViewModels.RssCreate;

namespace Droid.Screens.RssCreate
{
    public class RssCreateFragment : BaseFragment<RssCreateViewModel>
    {
        private RssCreateFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_rss_create;
        public override bool IsRoot => false;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.create_title);

            _viewHolder = new RssCreateFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                this.Bind(ViewModel, model => model.Url, fragment => fragment._viewHolder.EditText.Text)
                    .AddTo(disposable);

                ViewModel.Url.WhenAnyValue(s => s)
                    .Subscribe(s => _viewHolder.EditText.SetTextAndSetCursorToLast(s))
                    .AddTo(disposable);

                _viewHolder.EditText.GetEditorAction()
                    .Subscribe(action =>
                    {
                        if (action.ActionId == ImeAction.Done) _viewHolder.SendButton.CallOnClick();
                    })
                    .AddTo(disposable);

                this.BindCommand(ViewModel, model => model.CreateCommand, fragment => fragment._viewHolder.SendButton)
                    .AddTo(disposable);
            });

            return view;
        }
    }
}
