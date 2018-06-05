using mastodon.Consts;
using mastodon.Models;

namespace mastodon
{
    public partial class MastodonClient
    {
        public Relationships Follow(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Follow, accountId),
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Relationships Unfollow(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Unfollow, accountId),
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Account FollowRemote(string uri, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.FollowRemote,
                AccessToken = accessToken,
                Uri = uri,
            };
            return GetAuthenticatedData<Account>(param);
        }

        public Relationships Block(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Block, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Relationships Unblock(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Unblock, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Account[] GetBlocks(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetBlocks,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Relationships Mute(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Mute, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Account[] GetMutes(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetMutes,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Relationships Unmute(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Unmute, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }
    }
}