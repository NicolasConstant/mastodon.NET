using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Models;
using Newtonsoft.Json;

namespace mastodon
{
    public partial class MastodonClient
    {
        public async Task<Relationships> FollowAsync(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Follow, accountId);
            var result = await PostDataAsync(accessToken, route);
            return JsonConvert.DeserializeObject<Relationships>(result);
        }

        public async Task<Relationships> UnfollowAsync(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unfollow, accountId);
            var result = await PostDataAsync(accessToken, route);
            return JsonConvert.DeserializeObject<Relationships>(result);
        }

        public async Task<Account> FollowRemoteAsync(string uri, string accessToken)
        {
            var result = await PostDataAsync(accessToken, ApiRoutes.FollowRemote, new []{ new KeyValuePair<string, string>("uri", uri)});
            return JsonConvert.DeserializeObject<Account>(result);
        }

        public async Task<Relationships> BlockAsync(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Block, accountId);
            var result = await PostDataAsync(accessToken, route);
            return JsonConvert.DeserializeObject<Relationships>(result);
        }

        public async Task<Relationships> UnblockAsync(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unblock, accountId);
            var result = await PostDataAsync(accessToken, route);
            return JsonConvert.DeserializeObject<Relationships>(result);
        }

        public async Task<Account[]> GetBlocksAsync(string accessToken)
        {
            return await GetDataAsync<Account[]>(accessToken, ApiRoutes.GetBlocks);
        }

        public async Task<Relationships> MuteAsync(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Mute, accountId);
            var result = await PostDataAsync(accessToken, route);
            return JsonConvert.DeserializeObject<Relationships>(result);
        }

        public async Task<Account[]> GetMutesAsync(string accessToken)
        {
            return await GetDataAsync<Account[]>(accessToken, ApiRoutes.GetMutes);
        }

        public async Task<Relationships> UnmuteAsync(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unmute, accountId);
            var result = await PostDataAsync(accessToken, route);
            return JsonConvert.DeserializeObject<Relationships>(result);
        }
    }
}