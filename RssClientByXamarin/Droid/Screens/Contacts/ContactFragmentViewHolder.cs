using System;
using Android.Views;
using Android.Widget;
using Core.Extensions;
using Droid.NativeExtension;
using JetBrains.Annotations;

namespace Droid.Screens.Contacts
{
    public class ContactFragmentViewHolder
    {
        public ContactFragmentViewHolder([NotNull] View view, [NotNull] Func<View> getContactView)
        {
            RootLinearLayout = view.FindNotNull<LinearLayout>(Resource.Id.linearLayout_contacts_root);

            var viewContacts = getContactView.Invoke().NotNull();
            RootLinearLayout.AddView(viewContacts);
            TelegramContactViewHolder = new ContactItemViewHolder(viewContacts);

            viewContacts = getContactView.Invoke().NotNull();
            RootLinearLayout.AddView(viewContacts);
            EmailContactViewHolder = new ContactItemViewHolder(viewContacts);

            viewContacts = getContactView.Invoke().NotNull();
            RootLinearLayout.AddView(viewContacts);
            LinkedinContactViewHolder = new ContactItemViewHolder(viewContacts);

            viewContacts = getContactView.Invoke().NotNull();
            RootLinearLayout.AddView(viewContacts);
            DiscordContactViewHolder = new ContactItemViewHolder(viewContacts);
        }

        [NotNull] public LinearLayout RootLinearLayout { get; }

        [NotNull] public ContactItemViewHolder TelegramContactViewHolder { get; }

        [NotNull] public ContactItemViewHolder EmailContactViewHolder { get; }

        [NotNull] public ContactItemViewHolder LinkedinContactViewHolder { get; }

        [NotNull] public ContactItemViewHolder DiscordContactViewHolder { get; }
    }
}
