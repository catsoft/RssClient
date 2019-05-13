using Android.Content;
using Android.Gms.Common.Apis;
using Android.Gms.Wallet;
using Android.Gms.Wallet.Fragment;
using Android.Support.V4.App;

namespace Droid.Infrastructure.Payment
{
    public class PaymentFragmentProvider
    {
        public Fragment Resolve(Context context, double amount, GoogleApiClient.IConnectionCallbacks connectionCallbacks,
            GoogleApiClient.IOnConnectionFailedListener connectionFailedListener)
        {
//            var apiClient = new GoogleApiClient.Builder(context)
//                .AddConnectionCallbacks(connectionCallbacks)
//                .AddOnConnectionFailedListener(connectionFailedListener)
//                .AddApi(WalletClass.API)
//                .Build();

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
                        .SetPaymentMethodTokenizationType(WalletConstants.PaymentMethodTokenizationTypeDirect)
                        .AddParameter("gateway", "stripe")
                        .AddParameter("stripe:publishableKey", /**/ "dfasdfa")
                        .AddParameter("stripe:version", "1.15.1")
                        .Build())
                .SetShippingAddressRequired(true)
                .SetMerchantName("Xamarin")
                .SetPhoneNumberRequired(true)
                .SetShippingAddressRequired(true)
                .SetEstimatedTotalPrice("20.00")
                .SetCurrencyCode("RUB")
                .Build();

            walletFragment.Initialize(WalletFragmentInitParams.NewBuilder()
                .SetMaskedWalletRequest(maskedWalletRequest)
                .SetMaskedWalletRequestCode( /**/1)
                .Build());

            return walletFragment;
        }
    }
}