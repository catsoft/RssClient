using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Api.Feedly
{
    public class FeedlyCloudApiClient : IFeedlyCloudApiClient
    {
        private const int Count = 20;
        private const string Domen = "https://cloud.feedly.com/v3/";
        private const string Search = "search/feeds";

        public Task<FeedlyRssResponce> FindByQueryAsync(string query, CancellationToken token)
        {
            try
            {
//                var httpClient = new HttpClient();
//                
//                var response = await GetAsync($"{Domen}{Search}?query={query}&count={Count}", token).NotNull();
//
//                if (response.Content != null)
//                {
//                    var stringAsync = await response.Content.ReadAsStringAsync();
//
//                    var rssResponse = JsonConvert.DeserializeObject<FeedlyRssResponce>(stringAsync ?? "");
//
//                    return rssResponse;
//                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }
    }
}
