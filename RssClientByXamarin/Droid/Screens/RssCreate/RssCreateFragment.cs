using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Droid.Container;
using Droid.Screens.Base;
using Droid.Screens.Navigation;
using Shared.Infrastructure.Navigation;
using Shared.Repository.Rss;
using Shared.ViewModels.RssCreate;

namespace Droid.Screens.RssCreate
{
    public class RssCreateFragment : BaseFragment<RssCreateViewModel>
    {
        [Inject] private IRssRepository _rssRepository;

        [Inject] private INavigator _navigator;

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

            Activity.ShowKeyboard(urlView.EditText);

            return view;
        }
    }
}