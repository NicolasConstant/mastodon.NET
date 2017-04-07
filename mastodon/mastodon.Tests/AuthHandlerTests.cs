using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class AuthHandlerTests
    {
        [TestMethod]
        public void GetToken()
        {
            var authHandler = new AuthHandler(Settings.InstanceUrl);
            var tokenInfo = authHandler.GetTokenInfo(Settings.ClientId, Settings.ClientSecret, Settings.UserLogin, Settings.UserPassword);
        }
    }
}