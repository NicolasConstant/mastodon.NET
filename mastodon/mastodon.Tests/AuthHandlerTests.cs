using System.Net;
using System.Threading.Tasks;
using mastodon.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class AuthHandlerTests
    {
        [TestInitialize]
        public void TestInit()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
        }

        [TestMethod]
        public async Task GetToken()
        {
            var authHandler = new AuthHandler(Settings.InstanceName);
            var tokenInfo = await authHandler.GetTokenInfoAsync(Settings.ClientId, Settings.ClientSecret, Settings.UserLogin, Settings.UserPassword, AppScopeEnum.Read);
        }
    }
}