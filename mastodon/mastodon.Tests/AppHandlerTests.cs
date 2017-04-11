using System;
using System.Net;
using mastodon.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class AppHandlerTests
    {
        [TestInitialize]
        public void TestInit()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
        }

        [TestMethod]
        public void CreateApp()
        {
            var appHandler = new AppHandler(Settings.InstanceUrl);
            var scopes = AppScopeEnum.Read | AppScopeEnum.Write | AppScopeEnum.Follow;
            var appData = appHandler.CreateApp("MyAppName", "urn:ietf:wg:oauth:2.0:oob", scopes, "https://github.com/NicolasConstant/mastodon.NET");
        }
    }
}
