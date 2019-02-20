using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared.Api
{
    public class FeedlyCloudApiClient : HttpClient, IFeedlyCloudApiClient
    {
        private const string Domen = "https://cloud.feedly.com/v3/";
        private const string Search = "search/feeds";
        
        public async Task<FeedlyRssResponce> FindByQuery(string query)
        {
            var response = await GetAsync($"{Domen}{Search}?query={query}");

            var stream = await response.Content.ReadAsStringAsync();

            var rssResponse = JsonConvert.DeserializeObject<FeedlyRssResponce>(stream);
            
            return rssResponse;
        }
    }
}