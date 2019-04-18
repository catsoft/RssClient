using System;
using Android.Views;
using Android.Widget;

namespace Droid.Screens.Contacts
{
    public class ContactFragmentViewHolder
    {
        public ContactFragmentViewHolder(View view, Func<View> getContactView)
        {
            RootLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearLayout_contacts_root);

            var viewContacts = getContactView?.Invoke();
            RootLinearLayout.AddView(viewContacts);
            TelegramContactViewHolder = new ContactItemViewHolder(viewContacts);
            
            viewContacts = getContactView?.Invoke();
            RootLinearLayout.AddView(viewContacts);
            EmailContactViewHolder = new ContactItemViewHolder(viewContacts);
            
            viewContacts = getContactView?.Invoke();
            RootLinearLayout.AddView(viewContacts);
            LinkedinContactViewHolder = new ContactItemViewHolder(viewContacts);

            viewContacts = getContactView?.Invoke();
            RootLinearLayout.AddView(viewContacts);
            DiscordContactViewHolder = new ContactItemViewHolder(viewContacts);
        }
        
        public LinearLayout RootLinearLayout { get; }
        
        public ContactItemViewHolder TelegramContactViewHolder { get; }
        
        public ContactItemViewHolder EmailContactViewHolder { get; }
        
        public ContactItemViewHolder LinkedinContactViewHolder { get; }
        
        public ContactItemViewHolder DiscordContactViewHolder { get; }
    }
}