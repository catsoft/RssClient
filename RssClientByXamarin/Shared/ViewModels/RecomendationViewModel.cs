using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RecommendationViewModel : ViewModel
    {
        public abstract class Way : Way<RecommendationViewModel, Way.WayData>
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