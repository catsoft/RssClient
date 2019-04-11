using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.Settings
{
    public class SettingsViewModel : ViewModel
    {
        public abstract class Way : DataWay<SettingsViewModel, Way.WayData>
        {
            public class WayData
            {
            
            }
        }
    }
}