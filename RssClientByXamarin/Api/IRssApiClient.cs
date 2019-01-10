using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace Api
{
    public interface IRssApiClient
    {
        Task<SyndicationFeed> Update(string rssUrl);
    }
}
