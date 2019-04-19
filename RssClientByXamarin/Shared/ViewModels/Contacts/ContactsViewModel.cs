#region

using System.Reactive;
using System.Threading.Tasks;
using Droid.EmbeddedResourse;
using ReactiveUI;
using Shared.Infrastructure.ViewModels;
using Xamarin.Essentials;

#endregion

namespace Shared.ViewModels.Contacts
{
    public class ContactsViewModel : ViewModel
    {
        public ContactsViewModel()
        {
            GoTelegramCommand = ReactiveCommand.CreateFromTask(async () => await OpenLink(Strings.ContactsTelegramLink));
            GoMailCommand = ReactiveCommand.CreateFromTask(async () => await OpenLink(Strings.ContactsMailLink));
            GoLinkedinCommand = ReactiveCommand.CreateFromTask(async () => await OpenLink(Strings.ContactsLinkedInLink));
            GoDiscordCommand = ReactiveCommand.CreateFromTask(async () => await CopyToClipboard(Strings.ContactsDiscordLink));
        }

        public ReactiveCommand<Unit, Unit> GoTelegramCommand { get; }
        public ReactiveCommand<Unit, Unit> GoMailCommand { get; }
        public ReactiveCommand<Unit, Unit> GoLinkedinCommand { get; }
        public ReactiveCommand<Unit, Unit> GoDiscordCommand { get; }

        private async Task CopyToClipboard(string text)
        {
            await Clipboard.SetTextAsync(text);
            // TODO го клиборт
            //            Context.ToastClipboard(text);
        }

        private async Task OpenLink(string text)
        {
            if (await Launcher.CanOpenAsync(text))
                await Launcher.OpenAsync(text);
        }
    }
}
