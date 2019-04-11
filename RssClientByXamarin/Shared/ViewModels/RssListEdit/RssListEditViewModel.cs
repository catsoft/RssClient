using Shared.Infrastructure.Navigation;
using Shared.Infrastructure.ViewModels;

namespace Shared.ViewModels.RssListEdit
{
    public class RssListEditViewModel : ViewModel
    {
        public abstract class Way : DataWay<RssListEditViewModel, Way.WayData>
        {
            public class WayData
            {
            }
        }
    }
}