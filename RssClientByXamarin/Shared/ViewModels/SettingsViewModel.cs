using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public abstract class Way : Way<SettingsViewModel, Way.WayData>
        {
        
            public Way()
            {
                
            }
            
            public class WayData
            {
            
            }
        }
    }
}