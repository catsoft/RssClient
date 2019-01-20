using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public abstract class Way : Way<RssListViewModel, Way.WayData>
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