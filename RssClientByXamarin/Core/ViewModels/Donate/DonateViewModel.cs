using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Dialogs;
using Core.Infrastructure.ViewModels;
using Core.Resources;
using JetBrains.Annotations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Essentials;

namespace Core.ViewModels.Donate
{
    public class DonateViewModel : ViewModel
    {
        private readonly IToastService _toastService;

        public DonateViewModel(IToastService toastService)
        {
            _toastService = toastService;
            this.WhenAnyValue(model => model.Amount, model => model.Currency, (amount, currency) => $"Donate value: {amount} {currency}")
                .ToPropertyEx(this, model => model.PriceString);
            this.WhenAnyValue(model => model.Qiwy)
                .Select(w => $"Qiwi: {w}")
                .ToPropertyEx(this, model => model.QiwiString);
            
            QiwiCopyCommand = ReactiveCommand.CreateFromTask(DoQiwiCopy);
        }

        public string Amount { get; } = "69.0";

        public string Currency { get; } = "RUB";
        
        public string Qiwy { get; } = "+79326035155";

        public string Gateway { get; } = "stripe";

        public string PublishKey { get; } = "pk_test_XK3lj9w1QwRekmJaOpLrYMlo00jivh8zFO";

        public string StripeVersion { get; } = "2018-11-08";

        public extern string PriceString { [ObservableAsProperty] get; }
        
        public extern string QiwiString { [ObservableAsProperty] get; }
        
        [NotNull] public ReactiveCommand<Unit, Unit> QiwiCopyCommand { get; }

        private async Task DoQiwiCopy(CancellationToken token)
        {
            await Clipboard.SetTextAsync(Qiwy);
            _toastService.Show($"{Strings.CopyToClipboard} {Qiwy}");
        }
    }
}