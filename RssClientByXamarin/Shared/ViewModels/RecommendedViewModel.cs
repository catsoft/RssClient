using Shared.Database.Rss;
using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RecommendedViewModel : ViewModel
    {
        public abstract class Way : DataWay<Way.WayData>
        {
            public class WayData
            {
                public Categories Categories { get; }

                public WayData(Categories categories)
                {
                    Categories = categories;
                }
            }
        }   
    }
}