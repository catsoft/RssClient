using Core.Infrastructure.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Core.ViewModels.Donate
{
    public class DonateViewModel : ViewModel
    {
        public DonateViewModel()
        {
            this.WhenAnyValue(model => model.Amount, model => model.Currency, (amount, currency) => $"Donate value: {amount} {currency}");
        }

        public string Amount = "1.0";

        public string Currency = "RUB";

        public string Gateway = "stripe";

        public string PublishKey = "pk_test_XK3lj9w1QwRekmJaOpLrYMlo00jivh8zFO";

        public string StripeVersion = "2018-11-08";

        public extern string PriceString { [ObservableAsProperty] get; }
    }
}