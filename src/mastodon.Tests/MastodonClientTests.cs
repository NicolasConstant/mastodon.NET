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
        public void TestInit()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
        }

        [TestMethod]
        public async Task GetAccount()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var account = await client.GetAccountAsync(1, tokenInfo);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public async Task GetCurrentAccount()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var account = await client.GetCurrentAccountAsync(tokenInfo);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public async Task GetAccountFollowers()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var accounts = await client.GetAccountFollowersAsync(1, 4, tokenInfo);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }
        
        [TestMethod]
        public async Task GetAccountFollowing()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var accounts = await client.GetAccountFollowingAsync(1, tokenInfo, 4);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }
        
        [TestMethod]
        public async Task GetAccountRelationships()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var relationships = await client.GetAccountRelationshipsAsync(1, tokenInfo); //TODO pass a array Ids
            Assert.IsNotNull(relationships);
            Assert.IsTrue(relationships.First().id != default(int));
        }

        [TestMethod]
        public async Task GetFollowRequests()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var followRequests = await client.GetFollowRequestsAsync(tokenInfo);
            Assert.IsNotNull(followRequests);
        }
        
        [TestMethod]
        public async Task GetFavorites()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var favs = await client.GetFavoritesAsync(tokenInfo);
            Assert.IsNotNull(favs);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(favs.First().id));
        }

        [TestMethod]
        public async Task Follow()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var followedAccount = await client.FollowAsync(5, tokenInfo);
            Assert.IsNotNull(followedAccount);
            Assert.IsTrue(followedAccount.following);
        }

        [TestMethod]
        public async Task FollowRemote()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var followedAccount = await client.FollowRemoteAsync("@Gargron@mastodon.social", tokenInfo);
            Assert.IsNotNull(followedAccount);
            Assert.AreEqual("Eugen", followedAccount.display_name);
        }

        [TestMethod]
        public async Task Unfollow()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var unfollowedAccount = await client.UnfollowAsync(1, tokenInfo);
            Assert.IsNotNull(unfollowedAccount);
            Assert.IsFalse(unfollowedAccount.following);
        }

        [TestMethod]
        public async Task Block()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var blockedAccount = await client.BlockAsync(10, tokenInfo);
            Assert.IsNotNull(blockedAccount);
            Assert.IsTrue(blockedAccount.blocking);
        }

        [TestMethod]
        public async Task Unblock()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var unblockedAccount = await client.UnblockAsync(10, tokenInfo);
            Assert.IsNotNull(unblockedAccount);
            Assert.IsFalse(unblockedAccount.blocking);
        }

        [TestMethod]
        public async Task GetBlocks()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            await client.BlockAsync(2, tokenInfo);
            var blocks = await client.GetBlocksAsync(tokenInfo);
            Assert.IsNotNull(blocks);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(blocks.First().id));
        }

        [TestMethod]
        public async Task Mute()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var mutedAccount = await client.MuteAsync(1, tokenInfo);
            Assert.IsNotNull(mutedAccount);
            Assert.IsTrue(mutedAccount.muting);
        }

        [TestMethod]
        public async Task GetMutes()
        {
            await Mute(); //Make sure an account is muted
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var mutedAccounts = await client.GetMutesAsync(tokenInfo);
            Assert.IsNotNull(mutedAccounts);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mutedAccounts.First().username));
        }

        [TestMethod]
        public async Task Unmuted()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var unmutedAccount = await client.UnmuteAsync(1, tokenInfo);
            Assert.IsNotNull(unmutedAccount);
            Assert.IsFalse(unmutedAccount.muting);
        }

        [TestMethod]
        public async Task SearchAccount()
        {
            var q = "ale";
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var accounts = await client.SearchAccountsAsync(q, tokenInfo);
            Assert.IsNotNull(accounts);
            Assert.AreEqual(40, accounts.Length);
            Assert.IsTrue(accounts.First().username.ToLowerInvariant().Contains(q) || accounts.First().display_name.ToLowerInvariant().Contains(q));
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
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var notifications = await client.GetNotificationsAsync(tokenInfo);
            Assert.IsNotNull(notifications);
            Assert.IsFalse(string.IsNullOrWhiteSpace(notifications.First().type));
        }

        [TestMethod]
        public async Task GetSingleNotifications()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var notifications = await client.GetNotificationsAsync(tokenInfo);
            var notification = await client.GetSingleNotificationsAsync(tokenInfo, notifications.First().id);
            Assert.IsNotNull(notification);
            Assert.IsFalse(string.IsNullOrWhiteSpace(notification.type));
        }

        [TestMethod]
        public async Task ClearNotification()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            await client.ClearNotificationsAsync(tokenInfo);
        }

        private async Task<string> GetAccessToken()
        {
            if (string.IsNullOrWhiteSpace(Settings.Token))
            {
                var authHandler = new AuthHandler(Settings.InstanceName);
                var tokenInfo = await authHandler.GetTokenInfoAsync(Settings.ClientId, Settings.ClientSecret, Settings.UserEmail, Settings.UserPassword, AppScopeEnum.Write | AppScopeEnum.Read | AppScopeEnum.Follow);
                Settings.Token = tokenInfo.access_token;
            }
            
            return Settings.Token;
        }
    }
}