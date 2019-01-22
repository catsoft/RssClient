using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RecommendedCategoryListViewModel : ViewModel
    {
        public abstract class Way : Way<RecommendedCategoryListViewModel, Way.WayData>
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