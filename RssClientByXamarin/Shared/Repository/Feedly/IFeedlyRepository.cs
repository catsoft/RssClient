using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Shared.Api.Feedly;

namespace Shared.Repository.Feedly
{
    public interface IFeedlyRepository
    {
        Task<IEnumerable<FeedlyRss>> FindByQuery(string query, CancellationToken token);
    }
}