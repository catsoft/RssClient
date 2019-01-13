using Droid.Infrastructure;
using Shared.Database;

namespace Shared.ViewModels
{
    public class CloseViewModel : ViewModel
    {
        public abstract class CloseWay : Way<CloseViewModel, CloseWay.CloseWayData>
        {
            public CloseWay(RealmDatabase database)
            {
                
            }

            public class CloseWayData
            {
                
            }
        }
    }
}
