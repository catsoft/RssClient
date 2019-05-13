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
        public DonateFragment()
        {
        }
        
        protected override void RestoreState(Bundle saved)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = Strings.DonateTitle;
            
            _viewHolder = new DonateFragmentViewHolder(view);

            var paymentService = App.Container.Resolve<PaymentFragmentProvider>();
            var paymentFragment = paymentService.Resolve(Activity, 100, null, null);
            Activity.SupportFragmentManager.BeginTransaction().Add(_viewHolder.PayContainerLinearLayout.Id, paymentFragment).Commit();
            
//            OnActivation(disposable =>
//            {
//                this.BindCommand(ViewModel, model => model.PayCommand, fragment => fragment._viewHolder.PayButton)
//                    .AddTo(disposable);
//            });
            
            return view;
        }
    }
}