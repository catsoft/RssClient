using Android.OS;
using Android.Views;
using Autofac;
using Core;
using Core.Extensions;
using Core.Resources;
using Core.ViewModels.Donate;
using Droid.Infrastructure.Payment;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.Donate
{
    public class DonateFragment : BaseFragment<DonateViewModel>
    {
        [NotNull] private DonateFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_donate;

        public override bool IsRoot => true;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public DonateFragment() { }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            Title = Strings.DonateTitle;

            _viewHolder = new DonateFragmentViewHolder(view);

            var paymentService = App.Container.Resolve<PaymentFragmentProvider>().NotNull();

            view.Clickable = true;

            var paymentFragment = paymentService.Resolve(ViewModel.Amount,
                ViewModel.Currency,
                ViewModel.Gateway,
                ViewModel.PublishKey,
                ViewModel.StripeVersion);
            Activity.SupportFragmentManager.NotNull()
                .BeginTransaction()
                .NotNull()
                .Add(_viewHolder.PayContainerLinearLayout.Id, paymentFragment)
                .NotNull()
                .Commit();

            OnActivation(disposable =>
            {
                this.OneWayBind(ViewModel, model => model.PriceString, fragment => fragment._viewHolder.PriceTextView.Text)
                    .AddTo(disposable);

                this.OneWayBind(ViewModel, model => model.QiwiString, fragment => fragment._viewHolder.QiwiTextView.Text)
                    .AddTo(disposable);

                this.BindCommand(ViewModel, model => model.QiwiCopyCommand, fragment => fragment._viewHolder.CopyImageView)
                    .AddTo(disposable);
            });

            return view;
        }
    }
}