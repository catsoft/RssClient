using Droid.Infrastructure;
using Shared.Database.Rss;

namespace Shared.ViewModels
{
    public class RssItemDetailViewModel : ViewModel
    {
        public abstract class Way : Way<RssItemDetailViewModel, Way.DataModel>
        {
            public Way()
            {

            }

            public class DataModel
            {
                public RssModel RssModel { get; set; }

                public DataModel(RssModel rssModel)
                {
                    RssModel = rssModel;
                }
            }
        }
    }
}
