using System.Collections.Generic;
using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Models;

namespace mastodon
{
    public partial class MastodonClient
    {
        public async Task<Account[]> SearchAccountsAsync(string query, string accessToken, int limit = 40)
        {
            var parameters = new[]
            {
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("q", query)
            };
            return await GetDataAsync<Account[]>(accessToken, ApiRoutes.SearchForAccounts, parameters);
        }

        public async Task<Instance> GetInstanceAsync()
        {
            return await GetDataAsync<Instance>(null, ApiRoutes.GetInstance);
        }
    }
}