using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.Screens.Navigation;
using Xamarin.Essentials;
using Uri = Android.Net.Uri;

namespace Droid.Screens.Contacts
{
    public class ContactsFragment : TitleFragment
    {
        protected override int LayoutId => Resource.Layout.fragment_contacts;
        public override bool RootFragment => true;
        
        public ContactsFragment()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetString(Resource.String.contacts_title);

            var root = view.FindViewById<LinearLayout>(Resource.Id.linearLayout_contacts_root);

            InflateAndFill(inflater, root, Resource.Drawable.telegram_48, Resource.String.contacts_telegram, Resource.String.contacts_telegramLink);
            InflateAndFill(inflater, root, Resource.Drawable.email_48, Resource.String.contacts_mail, Resource.String.contacts_mailLink);

            return view;
        }

        private View InflateAndFill(LayoutInflater inflater, ViewGroup container, int imageId, int titleId, int linkId)
        {
            var linkView = inflater.Inflate(Resource.Layout.item_link_element_contacts, container, false);

            var linkText = Activity.GetText(linkId);
            linkView.Click += (sender, args) => OpenLink(linkText);

            var icon = linkView.FindViewById<ImageView>(Resource.Id.imageView_linkElementContacts_icon);
            var title = linkView.FindViewById<TextView>(Resource.Id.textView_linkElementContacts_title);

            icon.SetImageResource(imageId);
            var text = Context.GetText(titleId);
            title.Text = text;

            container.AddView(linkView);

            return linkView;
        }

        private async void OpenLink(string link)
        {
            if (await Launcher.CanOpenAsync(link))
                await Launcher.OpenAsync(link);
        }

        private void OpenTelegram()
        {

        }
    }
}