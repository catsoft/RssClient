using System.Collections.Generic;
using System.Globalization;
using Android.Gms.Wallet;
using Android.Gms.Wallet.Fragment;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Infrastructure.Payment
{
    public class PaymentFragmentProvider
    {
        public const int RequestCode = 666;

        public Fragment Resolve(string amount, string currency, string gateway, string publishKey, string version)
        {
            var walletFragment = SupportWalletFragment.NewInstance(WalletFragmentOptions.NewBuilder()
                .SetEnvironment(WalletConstants.EnvironmentTest)
                .SetMode(WalletFragmentMode.BuyButton)
                .SetTheme(WalletConstants.ThemeLight)
                .SetFragmentStyle(new WalletFragmentStyle()
                    .SetBuyButtonAppearance(WalletFragmentStyle.BuyButtonAppearance.AndroidPayDark)
                    .SetBuyButtonText(WalletFragmentStyle.BuyButtonText.DonateWith)
                    .SetBuyButtonWidth(WalletFragmentStyle.Dimension.MatchParent))
                .Build());

            var maskedWalletRequest = MaskedWalletRequest.NewBuilder()
                .SetPaymentMethodTokenizationParameters(
                    PaymentMethodTokenizationParameters.NewBuilder()
                        .SetPaymentMethodTokenizationType(WalletConstants.PaymentMethodTokenizationTypePaymentGateway)
                        .AddParameter("gateway", gateway)
                        .AddParameter("stripe:publishableKey", publishKey)
                        .AddParameter("stripe:version", version)
                        .Build())
                .SetEstimatedTotalPrice(amount.ToString(CultureInfo.InvariantCulture))
                .SetCurrencyCode(currency)
                .AddAllowedCardNetwork(WalletConstants.PaymentMethodCard)
                .AddAllowedCardNetwork(WalletConstants.PaymentMethodTokenizedCard)
                .AddAllowedCardNetworks(new List<Integer>()
                {
                    new Integer(WalletConstants.CardNetworkAmex),
                    new Integer(WalletConstants.CardNetworkDiscover),
                    new Integer(WalletConstants.CardNetworkVisa),
                    new Integer(WalletConstants.CardNetworkMastercard),
                })
                .Build();

            walletFragment.Initialize(WalletFragmentInitParams.NewBuilder()
                .SetMaskedWalletRequest(maskedWalletRequest)
                .SetMaskedWalletRequestCode(RequestCode)
                .Build());

            return walletFragment;
        }
    }
}