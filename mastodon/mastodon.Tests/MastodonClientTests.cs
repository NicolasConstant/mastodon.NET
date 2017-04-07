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

        private TokenInfo GetTokenInfo()
        {
            var authHandler = new AuthHandler(Settings.InstanceUrl);
            var tokenInfo = authHandler.GetTokenInfo(Settings.ClientId, Settings.ClientSecret, Settings.UserLogin,
                Settings.UserPassword);
            return tokenInfo;
        }
    }
}