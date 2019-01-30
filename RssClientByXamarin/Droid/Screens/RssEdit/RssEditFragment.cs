using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Droid.Container;
using Droid.Screens.Base;
using Droid.Screens.Navigation;
using RssClient.Repository;
using Shared.Services.Navigator;

namespace Droid.Screens.RssEdit
{
    public class RssEditFragment : WithKeyboardFragment
    {
        [Inject] private IRssRepository _rssRepository;

        [Inject] private INavigator _navigator;

        private string _itemId;

        protected override int LayoutId => Resource.Layout.fragment_rss_edit;
        public override bool RootFragment => false;

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

            var item = _rssRepository.Find(_itemId);

            var sendButton = view.FindViewById<Button>(Resource.Id.button_rssEdit_submit);
            sendButton.Click += SendButtonOnClick;

            var urlEditText = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);
            urlEditText.EditText.SetTextAndSetCursorToLast(item.Rss);
            urlEditText.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) sendButton.CallOnClick();
            };

            Activity.ShowKeyboard(urlEditText.EditText);

            return view;
        }

        private async void SendButtonOnClick(object sender, EventArgs eventArgs)
        {
            var urlView = View.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);

            var url = urlView.EditText.Text;

            await _rssRepository.Update(_itemId, url);

            _navigator.GoBack();
        }
    }
}