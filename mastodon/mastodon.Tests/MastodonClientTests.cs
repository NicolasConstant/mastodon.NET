using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class MastodonClientTests
    {
        [TestMethod]
        public void GetAccount()
        {
            var authHandler = new AuthHandler(Settings.InstanceUrl);
            var tokenInfo = authHandler.GetTokenInfo(Settings.ClientId, Settings.ClientSecret, Settings.UserLogin, Settings.UserPassword);
            
            var client = new MastodonClient(Settings.InstanceUrl);
            var account = client.GetAccount(1, tokenInfo.access_token);
        }
    }
}