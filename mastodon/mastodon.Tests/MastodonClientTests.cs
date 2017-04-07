using mastodon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class MastodonClientTests
    {
        [TestMethod]
        public void GetAccount()
        {
            var tokenInfo = GetTokenInfo();

            var client = new MastodonClient(Settings.InstanceUrl);
            var account = client.GetAccount(1, tokenInfo.access_token);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public void GetCurrentAccount()
        {
            var tokenInfo = GetTokenInfo();

            var client = new MastodonClient(Settings.InstanceUrl);
            var account = client.GetCurrentAccount(tokenInfo.access_token);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public void GetAccountFollowers()
        {
            var tokenInfo = GetTokenInfo();

            var client = new MastodonClient(Settings.InstanceUrl);
            var accounts = client.GetAccountFollowers(1, 4, tokenInfo.access_token);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }


        [TestMethod]
        public void GetAccountFollowing()
        {
            var tokenInfo = GetTokenInfo();

            var client = new MastodonClient(Settings.InstanceUrl);
            var accounts = client.GetAccountFollowing(1, tokenInfo.access_token, 4);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }

        [TestMethod]
        public void GetAccountStatuses()
        {
            var tokenInfo = GetTokenInfo();

            var client = new MastodonClient(Settings.InstanceUrl);
            var statuses1 = client.GetAccountStatuses(1, tokenInfo.access_token, 4);
            var statuses2 = client.GetAccountStatuses(1, tokenInfo.access_token, 4, true);
            var statuses3 = client.GetAccountStatuses(1, tokenInfo.access_token, 4, false, true);
        }

        private TokenInfo GetTokenInfo()
        {
            var authHandler = new AuthHandler(Settings.InstanceUrl);
            var tokenInfo = authHandler.GetTokenInfo(Settings.ClientId, Settings.ClientSecret, Settings.UserLogin,
                Settings.UserPassword);
            return tokenInfo;
        }
    }
}