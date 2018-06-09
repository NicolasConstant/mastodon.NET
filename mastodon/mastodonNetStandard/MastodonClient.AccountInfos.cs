using System.Collections.Generic;
using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Models;

namespace mastodon
{
    public partial class MastodonClient
    {
        public async Task<Account> GetAccountAsync(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.GetAccount, accountId);
            return await GetDataAsync<Account>(accessToken, route);
        }

        public async Task<Account> GetCurrentAccountAsync(string accessToken)
        {
            return await GetDataAsync<Account>(accessToken, ApiRoutes.GetCurrentAccount);
        }

        public async Task<Account[]> GetAccountFollowersAsync(int accountId, int limit, string accessToken)
        {
            var route = string.Format(ApiRoutes.GetAccountFollowers, accountId);
            return await GetDataAsync<Account[]>(accessToken, route, new []{ new KeyValuePair<string, string>("limit", limit.ToString())});
        }

        public async Task<Account[]> GetAccountFollowingAsync(int accountId, string accessToken, int limit)
        {
            var route = string.Format(ApiRoutes.GetAccountFollowing, accountId);
            return await GetDataAsync<Account[]>(accessToken, route, new[] { new KeyValuePair<string, string>("limit", limit.ToString()) });
        }

        public async Task<Relationships[]> GetAccountRelationshipsAsync(int accountId, string accessToken)
        {
            return await GetDataAsync<Relationships[]>(accessToken, ApiRoutes.GetAccountRelationships, new[] { new KeyValuePair<string, string>("id", accountId.ToString()) });
        }

        public async Task<Account[]> GetFollowRequestsAsync(string accessToken)
        {
            return await GetDataAsync<Account[]>(accessToken, ApiRoutes.GetFollowRequests);
        }

        public async Task AuthorizeFollowRequestAsync(int id, string accessToken)
        {
            await PostDataAsync(accessToken, ApiRoutes.AuthorizeFollowRequest, new [] { new KeyValuePair<string, string>("id", id.ToString()) });
        }

        public async Task RejectFollowRequestAsync(int id, string accessToken)
        {
            await PostDataAsync(accessToken, ApiRoutes.RejectFollowRequest, new[] { new KeyValuePair<string, string>("id", id.ToString()) });
        }
    }
}