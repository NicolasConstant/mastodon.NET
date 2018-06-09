using System.Configuration;
using System.Diagnostics;
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
            using (var authHandler = new AuthHandler(Settings.InstanceName))
            {
                var tokenInfo = await authHandler.GetTokenInfoAsync(Settings.ClientId, Settings.ClientSecret,
                    Settings.UserEmail, Settings.UserPassword, AppScopeEnum.Read);
                Assert.IsNotNull(tokenInfo);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(tokenInfo.access_token));
            }
        }

        [TestMethod]
        public void GetOauthUrl()
        {
            var instanceName = Settings.InstanceName;
            var clientId = Settings.ClientId;
            var authHandler = new AuthHandler(instanceName);
            var url = authHandler.GetOauthCodeUrl(clientId, AppScopeEnum.Read | AppScopeEnum.Write);
            Assert.AreEqual($"https://{instanceName}/oauth/authorize?scope=read%20write&response_type=code&redirect_uri=urn:ietf:wg:oauth:2.0:oob&client_id={clientId}", url);
        }

        [TestMethod]
        public async Task GetTokenWithCode()
        {
            var authHandler = new AuthHandler(Settings.InstanceName);
            var token = await authHandler.GetTokenInfoAsync(Settings.ClientId, Settings.ClientSecret, Settings.OauthCode);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(token.access_token));
        }
    }
}