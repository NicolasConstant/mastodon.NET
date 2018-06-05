using mastodon.Consts;
using mastodon.Models;
using RestSharp.Portable;

namespace mastodon
{
    public partial class MastodonClient
    {
        public Account GetAccount(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetAccount, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account>(param);
        }

        public Account GetCurrentAccount(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetCurrentAccount,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account>(param);
        }

        public Account[] GetAccountFollowers(int accountId, int limit, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetAccountFollowers, accountId),
                AccessToken = accessToken,
                Limit = limit
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Account[] GetAccountFollowing(int accountId, string accessToken, int limit = -1)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetAccountFollowing, accountId),
                AccessToken = accessToken,
                Limit = limit
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Relationships[] GetAccountRelationships(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetAccountRelationships,
                AccessToken = accessToken,
                Id = accountId
            };
            return GetAuthenticatedData<Relationships[]>(param);
        }

        public Account[] GetFollowRequests(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetFollowRequests,
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public void AuthorizeFollowRequest(int id, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.AuthorizeFollowRequest,
                AccessToken = accessToken,
                Id = id
            };
            GetAuthenticatedData<object>(param);
        }

        public void RejectFollowRequest(int id, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.RejectFollowRequest,
                AccessToken = accessToken,
                Id = id
            };
            GetAuthenticatedData<object>(param);
        }
    }
}