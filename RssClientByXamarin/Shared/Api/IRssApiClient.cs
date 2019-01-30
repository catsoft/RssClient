using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace Shared.Api
{
    public interface IRssApiClient
    {
        Task<SyndicationFeed> Update(string rssUrl);
    }
}