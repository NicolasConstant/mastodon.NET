﻿using System;
using System.Net;
using System.Threading.Tasks;
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
        public async Task CreateApp()
        {
            var appHandler = new AppHandler(Settings.InstanceName);
            var scopes = AppScopeEnum.Read | AppScopeEnum.Write | AppScopeEnum.Follow;
            var appData = await appHandler.CreateAppAsync("mastodon.NET", "urn:ietf:wg:oauth:2.0:oob", scopes, "https://github.com/NicolasConstant/mastodon.NET");
            Settings.ClientId = appData.client_id;
            Settings.ClientSecret = appData.client_secret;
        }
    }
}
