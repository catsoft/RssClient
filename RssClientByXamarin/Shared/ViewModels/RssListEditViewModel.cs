using Shared.Services.Navigator;

namespace Shared.ViewModels
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