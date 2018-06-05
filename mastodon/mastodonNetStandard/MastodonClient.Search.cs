using mastodon.Consts;
using mastodon.Models;
using RestSharp.Portable;

namespace mastodon
{
    public partial class MastodonClient
    {
        public Account[] SearchAccounts(string query, string accessToken, int limit = 40)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.SearchForAccounts,
                AccessToken = accessToken,
                Limit = limit,
                Query = query
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Instance GetInstance()
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetInstance,
            };
            return GetAuthenticatedData<Instance>(param);
        }
    }
}