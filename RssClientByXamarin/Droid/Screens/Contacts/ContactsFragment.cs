using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Navigation;
using Xamarin.Essentials;

namespace Droid.Screens.Contacts
{
    public class ContactsFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_contacts;
        public override bool RootFragment => true;
        
        public ContactsFragment()
        {
            
        }
        
        protected override void RestoreState(Bundle saved)
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetString(Resource.String.contacts_title);

            var root = view.FindViewById<LinearLayout>(Resource.Id.linearLayout_contacts_root);

            InflateAndFill(inflater, root, Resource.Drawable.telegram_48, Resource.String.contacts_telegram, () => OpenLink(Resource.String.contacts_telegramLink));
            InflateAndFill(inflater, root, Resource.Drawable.email_48, Resource.String.contacts_mail, () => OpenLink(Resource.String.contacts_mailLink));
            InflateAndFill(inflater, root, Resource.Drawable.linkedin_48, Resource.String.contacts_linkedIn, () => OpenLink(Resource.String.contacts_linkedInLink));
            InflateAndFill(inflater, root, Resource.Drawable.discord_48, Resource.String.contacts_discord, () => CopyToClipboard(Resource.String.contacts_discordLink));

            return view;
        }

        private View InflateAndFill(LayoutInflater inflater, ViewGroup container, int imageId, int titleId, Action action)
        {
            var linkView = inflater.Inflate(Resource.Layout.item_link_element_contacts, container, false);

            linkView.Click += ((sender, args) => action?.Invoke());

            var icon = linkView.FindViewById<ImageView>(Resource.Id.imageView_linkElementContacts_icon);
            var title = linkView.FindViewById<TextView>(Resource.Id.textView_linkElementContacts_title);

            icon.SetImageResource(imageId);
            var text = Context.GetText(titleId);
            title.Text = text;

            container.AddView(linkView);

            return linkView;
        }

        private async void CopyToClipboard(int linkId)
        {
            var text = Activity.GetText(linkId);
            await Clipboard.SetTextAsync(text);
            
            Context.ToastClipboard(text);
        }

        private async void OpenLink(int linkId)
        {
            var text = Activity.GetText(linkId);
            if (await Launcher.CanOpenAsync(text))
                await Launcher.OpenAsync(text);
        }
    }
}