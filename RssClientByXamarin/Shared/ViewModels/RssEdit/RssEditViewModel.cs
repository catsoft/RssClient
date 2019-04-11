using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssEdit
{
    public class RssEditViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssEditViewModel, Way.WayData>
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