using Android.OS;
using Android.Views;
using Droid.Screens.Navigation;
using ReactiveUI;
using Shared.Extensions;
using Shared.ViewModels.Contacts;

namespace Droid.Screens.Contacts
{
    public class ContactsFragment : BaseFragment<ContactsViewModel>
    {
        private ContactFragmentViewHolder _viewHolder;

        // ReSharper disable once EmptyConstructor
        public ContactsFragment() { }

        protected override int LayoutId => Resource.Layout.fragment_contacts;
        public override bool IsRoot => true;

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Activity.GetString(Resource.String.contacts_title);

            _viewHolder = new ContactFragmentViewHolder(view,
                () => inflater.Inflate(Resource.Layout.item_link_element_contacts, container, false));

            _viewHolder.TelegramContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.telegram_48);
            _viewHolder.EmailContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.email_48);
            _viewHolder.DiscordContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.discord_48);
            _viewHolder.LinkedinContactViewHolder.IconImageView.SetImageResource(Resource.Drawable.linkedin_48);

            _viewHolder.TelegramContactViewHolder.TitleTextView.SetText(Resource.String.contacts_telegram);
            _viewHolder.EmailContactViewHolder.TitleTextView.SetText(Resource.String.contacts_mail);
            _viewHolder.LinkedinContactViewHolder.TitleTextView.SetText(Resource.String.contacts_linkedIn);
            _viewHolder.DiscordContactViewHolder.TitleTextView.SetText(Resource.String.contacts_discord);

            OnActivation(disposable =>
            {
                this.BindCommand(ViewModel,
                        model => model.GoTelegramCommand,
                        fragment => fragment._viewHolder.TelegramContactViewHolder.RootView)
                    .AddTo(disposable);

                this.BindCommand(ViewModel,
                        model => model.GoMailCommand,
                        fragment => fragment._viewHolder.EmailContactViewHolder.RootView)
                    .AddTo(disposable);

                this.BindCommand(ViewModel,
                        model => model.GoDiscordCommand,
                        fragment => fragment._viewHolder.DiscordContactViewHolder.RootView)
                    .AddTo(disposable);

                this.BindCommand(ViewModel,
                        model => model.GoLinkedinCommand,
                        fragment => fragment._viewHolder.LinkedinContactViewHolder.RootView)
                    .AddTo(disposable);
            });

            return view;
        }
    }
}
