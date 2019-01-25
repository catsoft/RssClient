using Shared.Database.Rss;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RecommendedViewModel : ViewModel
    {
        public abstract class Way : Way<RecommendedViewModel, Way.WayData>
        {
        
            public Way()
            {
                
            }
            
            public class WayData
            {
                public Categories Categories { get; set; }

                public WayData(Categories categories)
                {
                    Categories = categories;
                }
            }
        }   
    }
}