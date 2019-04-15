using System.Reactive;
using System.Threading.Tasks;
using Android.App;
using ReactiveUI;
using Shared.Infrastructure.ViewModels;
using Xamarin.Essentials;

namespace Shared.ViewModels.Contacts
{
    public class ContactsViewModel : ViewModel
    {
        public ContactsViewModel()
        {
            GoTelegramCommand = ReactiveCommand.CreateFromTask(() => OpenLink("1"));
            GoMailCommand = ReactiveCommand.CreateFromTask(() => OpenLink("2"));
            GoLinkedinCommand = ReactiveCommand.CreateFromTask(() => OpenLink("3"));
            GoDiscordCommand = ReactiveCommand.CreateFromTask(() => CopyToClipboard("4"));
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