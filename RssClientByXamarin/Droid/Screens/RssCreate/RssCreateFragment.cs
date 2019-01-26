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
using Droid.Screens.Navigation;
using RssClient.Repository;
using Shared;
using Shared.Services.Navigator;

namespace Droid.Screens.RssCreate
{
    public class RssCreateFragment : TitleFragment
    {
        [Inject]
	    private IRssRepository _rssRepository;
        
        [Inject]
        private INavigator _navigator;

        protected override int LayoutId => Resource.Layout.fragment_rss_create;
        public override bool RootFragment => true;

        public RssCreateFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Title = GetText(Resource.String.create_title);

            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var sendButton = view.FindViewById<Button>(Resource.Id.button_rssCreate_submit);
            sendButton.Click += async (sender, args) =>
            {
                var url = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssCreate_link);

                await _rssRepository.InsertByUrl(url.EditText.Text);

                _navigator.GoBack();
            };
            
            var urlView = view.FindViewById<TextInputLayout>(Resource.Id.textInputLayout_rssCreate_link);
            urlView.EditText.SetTextAndSetCursorToLast(GetText(Resource.String.create_urlDefault));
            urlView.EditText.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done) sendButton.CallOnClick();
            };
            
            return view;
        }
    }
}