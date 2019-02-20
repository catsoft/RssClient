using System.Threading.Tasks;

namespace Shared.Api
{
    public interface IFeedlyCloudApiClient
    {
        Task<FeedlyRssResponce> FindByQuery(string query);
    }
}