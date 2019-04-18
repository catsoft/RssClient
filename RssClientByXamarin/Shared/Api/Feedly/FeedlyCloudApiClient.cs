using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared.Api.Feedly
{
    public class FeedlyCloudApiClient : HttpClient, IFeedlyCloudApiClient
    {
        private const int Count = 20;
        private const string Domen = "https://cloud.feedly.com/v3/";
        private const string Search = "search/feeds";
        
        public async Task<FeedlyRssResponce> FindByQueryAsync(string query, CancellationToken token)
        {
            var response = await GetAsync($"{Domen}{Search}?query={query}&count={Count}", token);

            var stream = await response.Content.ReadAsStringAsync();

            var rssResponse = JsonConvert.DeserializeObject<FeedlyRssResponce>(stream);
            
            return rssResponse;
        }
    }
}