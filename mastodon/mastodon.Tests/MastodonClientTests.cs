using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using mastodon.Enums;
using mastodon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class MastodonClientTests
    {
        [TestInitialize]
        public async Task TestInit()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
        }

        [TestMethod]
        public async Task GetAccount()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var account = await client.GetAccountAsync(1, tokenInfo.access_token);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public async Task GetCurrentAccount()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var account = await client.GetCurrentAccountAsync(tokenInfo.access_token);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public async Task GetAccountFollowers()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var accounts = await client.GetAccountFollowersAsync(1, 4, tokenInfo.access_token);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }
        
        [TestMethod]
        public async Task GetAccountFollowing()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var accounts = await client.GetAccountFollowingAsync(1, tokenInfo.access_token, 4);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }

        [TestMethod]
        public async Task GetAccountStatuses()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var statuses1 = await client.GetAccountStatusesAsync(1, tokenInfo.access_token, 4);
            var statuses2 = await client.GetAccountStatusesAsync(1, tokenInfo.access_token, 4, true);
            var statuses3 = await client.GetAccountStatusesAsync(1, tokenInfo.access_token, 4, false, true);

            Assert.AreEqual(4, statuses1.Length);
            Assert.AreEqual(4, statuses2.Length);
            Assert.AreEqual(4, statuses3.Length);
        }

        [TestMethod]
        public async Task GetAccountRelationships()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var relationships = await client.GetAccountRelationshipsAsync(1, tokenInfo.access_token); //TODO pass a array Ids
            Assert.IsNotNull(relationships);
            Assert.IsTrue(relationships.First().id != default(int));
        }

        [TestMethod]
        public async Task GetFollowRequests()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var followRequests = await client.GetFollowRequestsAsync(tokenInfo.access_token);
            Assert.IsNotNull(followRequests);
        }

        [TestMethod]
        public async Task GetHomeTimeline()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var timeline = await client.GetHomeTimelineAsync(tokenInfo.access_token);
        }

        [TestMethod]
        public async Task GetPublicTimeline()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var timeline1 = await client.GetPublicTimelineAsync(tokenInfo.access_token);
            var timeline2 = await client.GetPublicTimelineAsync(tokenInfo.access_token, true);
        }

        [TestMethod]
        public async Task GetHastagTimeline()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var timeline1 = await client.GetHastagTimelineAsync("mastodon", tokenInfo.access_token);
            var timeline2 = await client.GetHastagTimelineAsync("mastodon", tokenInfo.access_token, true);
        }

        [TestMethod]
        public async Task<Status> PostNewStatus()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Write);

            var client = new MastodonClient(Settings.InstanceName);
            var status = await client.PostNewStatusAsync(tokenInfo.access_token, "Cool status for testing purpose", StatusVisibilityEnum.Private, -1, null, true, "TESTING SPOILER");
            Assert.IsNotNull(status.content);
            return status;
        }

        [TestMethod]
        public async Task DeleteStatus()
        {
            var status = await PostNewStatus();

            var tokenInfo = await GetTokenInfo(AppScopeEnum.Write);

            var client = new MastodonClient(Settings.InstanceName);
            await client.DeleteStatusAsync(tokenInfo.access_token, status.id);
        }

        [TestMethod]
        public async Task GetFavorites()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var favs = await client.GetFavoritesAsync(tokenInfo.access_token);
            Assert.IsNotNull(favs);
        }

        [TestMethod]
        public async Task Follow()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var followedAccount = await client.FollowAsync(1, tokenInfo.access_token);
            Assert.IsNotNull(followedAccount);
            Assert.IsTrue(followedAccount.following);
        }

        [TestMethod]
        public async Task FollowRemote()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var followedAccount = await client.FollowRemoteAsync("@Gargron@mastodon.social", tokenInfo.access_token);
            Assert.IsNotNull(followedAccount);
            Assert.AreEqual("Eugen", followedAccount.display_name);
        }

        [TestMethod]
        public async Task Unfollow()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var unfollowedAccount = await client.UnfollowAsync(1, tokenInfo.access_token);
            Assert.IsNotNull(unfollowedAccount);
            Assert.IsFalse(unfollowedAccount.following);
        }

        [TestMethod]
        public async Task Block()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var blockedAccount = await client.BlockAsync(10, tokenInfo.access_token);
            Assert.IsNotNull(blockedAccount);
            Assert.IsTrue(blockedAccount.blocking);
        }

        [TestMethod]
        public async Task Unblock()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var unblockedAccount = await client.UnblockAsync(10, tokenInfo.access_token);
            Assert.IsNotNull(unblockedAccount);
            Assert.IsFalse(unblockedAccount.blocking);
        }

        [TestMethod]
        public async Task GetBlocks()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var blocks = await client.GetBlocksAsync(tokenInfo.access_token);
            Assert.IsNotNull(blocks);
        }

        [TestMethod]
        public async Task Mute()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var mutedAccount = await client.MuteAsync(2, tokenInfo.access_token);
            Assert.IsNotNull(mutedAccount);
            Assert.IsTrue(mutedAccount.muting);
        }

        [TestMethod]
        public async Task GetMutes()
        {
            Mute(); //Make sure an account is muted
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var mutedAccounts = await client.GetMutesAsync(tokenInfo.access_token);
            Assert.IsNotNull(mutedAccounts);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mutedAccounts.First().username));
        }

        [TestMethod]
        public async Task Unmuted()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceName);
            var unmutedAccount = await client.UnmuteAsync(2, tokenInfo.access_token);
            Assert.IsNotNull(unmutedAccount);
            Assert.IsFalse(unmutedAccount.muting);
        }

        [TestMethod]
        public async Task SearchAccount()
        {
            var q = "ale";
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var accounts = await client.SearchAccountsAsync(q, tokenInfo.access_token);
            Assert.IsNotNull(accounts);
            Assert.AreEqual(40, accounts.Length);
            Assert.IsTrue(accounts.First().username.Contains(q) || accounts.First().display_name.Contains(q));
        }

        [TestMethod]
        public async Task GetInstance()
        {
            var client = new MastodonClient(Settings.InstanceName);
            var instance = await client.GetInstanceAsync();
            Assert.IsNotNull(instance);
            Assert.IsFalse(string.IsNullOrWhiteSpace(instance.uri));
        }

        [TestMethod]
        public async Task GetNotifications()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var notifications = await client.GetNotificationsAsync(tokenInfo.access_token);
            Assert.IsNotNull(notifications);
            Assert.IsFalse(string.IsNullOrWhiteSpace(notifications.First().type));
        }

        [TestMethod]
        public async Task GetSingleNotifications()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            var notification = await client.GetSingleNotificationsAsync(tokenInfo.access_token, 114760);
            Assert.IsNotNull(notification);
            Assert.IsFalse(string.IsNullOrWhiteSpace(notification.type));
        }

        [TestMethod]
        public async Task ClearNotification()
        {
            var tokenInfo = await GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceName);
            await client.ClearNotificationsAsync(tokenInfo.access_token);
        }

        private async Task<TokenInfo> GetTokenInfo(AppScopeEnum scope)
        {
            var authHandler = new AuthHandler(Settings.InstanceName);
            var tokenInfo = await authHandler.GetTokenInfoAsync(Settings.ClientId, Settings.ClientSecret, Settings.UserLogin, Settings.UserPassword, scope);
            return tokenInfo;
        }
    }
}