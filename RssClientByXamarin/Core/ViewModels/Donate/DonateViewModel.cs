using System.Reactive;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using JetBrains.Annotations;
using ReactiveUI;

namespace Core.ViewModels.Donate
{
    public class DonateViewModel : ViewModel
    {
        public DonateViewModel()
        {
            PayCommand = ReactiveCommand.Create<double>(DoPay).NotNull();
        }
        
        [NotNull] public ReactiveCommand<double, Unit> PayCommand { get; }

        private void DoPay(double amount)
        {
        }
    }
}