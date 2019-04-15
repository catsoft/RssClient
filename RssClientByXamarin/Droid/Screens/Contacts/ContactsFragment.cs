using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Droid.NativeExtension;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Extensions;
using Shared.ViewModels.Contacts;
using Xamarin.Essentials;

namespace Droid.Screens.Contacts
{
    public class ContactsFragment : BaseFragment<ContactsViewModel>
    {
        protected override int LayoutId => Resource.Layout.fragment_contacts;
        public override bool IsRoot => true;

        public ContactFragmentViewHolder ViewHolder { get; private set; }
        
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

            ViewHolder = new ContactFragmentViewHolder(view,
                () => inflater.Inflate(Resource.Layout.item_link_element_contacts, container, false));
            
            ViewHolder.TelegramContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.telegram_48);
            ViewHolder.EmailContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.email_48);
            ViewHolder.DiscordContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.discord_48);
            ViewHolder.LinkedinContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.linkedin_48);
            
            ViewHolder.TelegramContactViewHolder.TitleTextView.SetText(Resource.String.contacts_telegram);
            ViewHolder.EmailContactViewHolder.TitleTextView.SetText(Resource.String.contacts_mail);
            ViewHolder.LinkedinContactViewHolder.TitleTextView.SetText(Resource.String.contacts_linkedIn);
            ViewHolder.DiscordContactViewHolder.TitleTextView.SetText(Resource.String.contacts_discord);

            OnActivation(disposable =>
            {
                this.Bind(ViewModel, model => model.GoTelegramCommand,
                    fragment => fragment.ViewHolder.TelegramContactViewHolder.RootView)
                    .AddTo(disposable);
                
                this.Bind(ViewModel, model => model.GoMailCommand,
                        fragment => fragment.ViewHolder.EmailContactViewHolder.RootView)
                    .AddTo(disposable);
                
                this.Bind(ViewModel, model => model.GoDiscordCommand,
                        fragment => fragment.ViewHolder.DiscordContactViewHolder.RootView)
                    .AddTo(disposable);
                
                this.Bind(ViewModel, model => model.GoLinkedinCommand,
                        fragment => fragment.ViewHolder.LinkedinContactViewHolder.RootView)
                    .AddTo(disposable);
            });

            return view;
        }
    }
}