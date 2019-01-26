using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Autofac;
using Droid.Container;
using Droid.Screens.Base;
using Droid.Screens.Close;
using Droid.Screens.Navigation;
using RssClient.Repository;
using Shared;
using Shared.Database.Rss;
using Shared.Services.Navigator;
using Shared.ViewModels;

namespace Droid.Screens.RssEdit
{
    public class RssEditFragment : TitleFragment
    {
        [Inject]
        private IRssRepository _rssRepository;

        [Inject] 
        private INavigator _navigator;
        
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

        public override void OnViewStateRestored(Bundle savedInstanceState)
        {
            base.OnViewStateRestored(savedInstanceState);

            if (savedInstanceState != null)
            {
                _itemId = savedInstanceState.GetString(nameof(_itemId));
            }
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Title = GetText(Resource.String.edit_title);

            var item = _rssRepository.Find(_itemId);

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var sendButton = view.FindViewById<Button>(Resource.Id.button_rssEdit_submit);
            sendButton.Click += SendButtonOnClick;
            
            var urlEditText = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssEdit_link);
            urlEditText.EditText.SetTextAndSetCursorToLast(item.Rss);
            urlEditText.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) sendButton.CallOnClick();
            };
            
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