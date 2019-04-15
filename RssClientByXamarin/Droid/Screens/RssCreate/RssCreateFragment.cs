using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Base;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Extensions;
using Shared.ViewModels.RssCreate;

namespace Droid.Screens.RssCreate
{
    public class RssCreateFragment : BaseFragment<RssCreateViewModel>
    {
        private Button _sendButton;
        private TextInputLayout _urlTextInputLayout;

        protected override int LayoutId => Resource.Layout.fragment_rss_create;
        public override bool IsRoot => false;

        public RssCreateFragment()
        {
        }

        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Title = GetText(Resource.String.create_title);

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _sendButton = view.FindViewById<Button>(Resource.Id.button_rssCreate_submit);
            _urlTextInputLayout = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssCreate_link);

            OnActivation(disposable =>
            {
                this.Bind(ViewModel, model => model.Url, fragment => fragment._urlTextInputLayout.EditText.Text)
                    .AddTo(disposable);

                ViewModel.Url.WhenAnyValue(s => s)
                    .Subscribe(s => _urlTextInputLayout.EditText.SetTextAndSetCursorToLast(s))
                    .AddTo(disposable);

                _urlTextInputLayout.EditText.GetEditorAction().Subscribe(action =>
                {
                    if (action.ActionId == ImeAction.Done) _sendButton.CallOnClick();
                }).AddTo(disposable);

                this.BindCommand(ViewModel, model => model.CreateCommand,
                    fragment => fragment._sendButton).AddTo(disposable);
            });

            return view;
        }
    }
}