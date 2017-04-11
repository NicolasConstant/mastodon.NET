using System;
using mastodon.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class AppHandlerTests
    {
        [TestMethod]
        public void CreateApp()
        {
            var appHandler = new AppHandler(Settings.InstanceUrl);
            var appData = appHandler.CreateApp("MyAppName", "urn:ietf:wg:oauth:2.0:oob", AppScopeEnum.Write, "https://github.com/NicolasConstant/mastodon.NET");
        }
    }
}
