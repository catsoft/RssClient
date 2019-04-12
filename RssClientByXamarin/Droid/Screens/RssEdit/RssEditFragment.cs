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
using Shared.ViewModels.RssEdit;

namespace Droid.Screens.RssEdit
{
    public class RssEditFragment : BaseFragment<RssEditViewModel>
    {
        private string _itemId;
        private TextInputLayout _urlEditText;
        private Button _sendButton;

        protected override int LayoutId => Resource.Layout.fragment_rss_edit;
        public override bool IsRoot => false;

        public RssEditFragment()
        {

        }

        public RssEditFragment(string itemId)
        {
            _itemId = itemId;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutString(nameof(_itemId), _itemId);
        }

        protected override void RestoreState(Bundle saved)
        {
            _itemId = saved.GetString(nameof(_itemId));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.edit_title);

            _sendButton = view.FindViewById<Button>(Resource.Id.button_rssEdit_submit);
            _urlEditText = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);
            
            OnActivation(compositeDisposable =>
                {
                    this.Bind(ViewModel, model => model.Url, fragment => fragment._urlEditText.EditText.Text).AddTo(compositeDisposable);

                    ViewModel.Url.WhenAnyValue(s => s)
                        .Subscribe(s => _urlEditText.EditText.SetTextAndSetCursorToLast(s))
                        .AddTo(compositeDisposable);

                    _urlEditText.EditText.GetEditorAction().Subscribe(action =>
                    {
                        if (action.ActionId == ImeAction.Done) _sendButton.CallOnClick();
                    });

                    this.BindCommand(ViewModel, model => model.UpdateCommand, activity => activity._sendButton)
                        .AddTo(compositeDisposable);
                    
                    ViewModel.LoadCommand.ExecuteNow().AddTo(compositeDisposable);
                }
            );
            
            return view;
        }
    }
}