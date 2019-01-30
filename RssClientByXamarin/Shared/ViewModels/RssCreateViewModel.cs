using Shared.Services.Navigator;

namespace Shared.ViewModels
{
    public class RssCreateViewModel : ViewModel
    {
        public abstract class Way : Way<Way.WayData>
        {
            public class WayData
            {
                public string RssId { get; }

                public WayData(string rssId)
                {
                    RssId = rssId;
                }
            }
        }
    }
}