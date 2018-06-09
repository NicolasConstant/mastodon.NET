using mastodon.Enums;
using mastodon.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests.Tools
{
    [TestClass]
    public class AppScopesConverterTests
    {
        [TestMethod]
        public void ConvertRead()
        {
            var enumWithNoFlag = AppScopeEnum.Read;
            var value = AppScopesConverter.GetScopes(enumWithNoFlag);
            Assert.AreEqual("read", value);
        }

        [TestMethod]
        public void ConvertWrite()
        {
            var enumWithNoFlag = AppScopeEnum.Write;
            var value = AppScopesConverter.GetScopes(enumWithNoFlag);
            Assert.AreEqual("write", value);
        }

        [TestMethod]
        public void ConvertFollow()
        {
            var enumWithNoFlag = AppScopeEnum.Follow;
            var value = AppScopesConverter.GetScopes(enumWithNoFlag);
            Assert.AreEqual("follow", value);
        }

        [TestMethod]
        public void ConvertReadFollow()
        {
            var enumWithNoFlag = AppScopeEnum.Read | AppScopeEnum.Follow;
            var value = AppScopesConverter.GetScopes(enumWithNoFlag);
            Assert.IsTrue(value.Contains("read"));
            Assert.IsFalse(value.Contains("write"));
            Assert.IsTrue(value.Contains("follow"));
        }

        [TestMethod]
        public void ConvertReadWrite()
        {
            var enumWithNoFlag = AppScopeEnum.Read | AppScopeEnum.Write;
            var value = AppScopesConverter.GetScopes(enumWithNoFlag);
            Assert.IsTrue(value.Contains("read"));
            Assert.IsTrue(value.Contains("write"));
            Assert.IsFalse(value.Contains("follow"));
        }

        [TestMethod]
        public void ConvertWriteFollow()
        {
            var enumWithNoFlag = AppScopeEnum.Write | AppScopeEnum.Follow;
            var value = AppScopesConverter.GetScopes(enumWithNoFlag);
            Assert.IsFalse(value.Contains("read"));
            Assert.IsTrue(value.Contains("write"));
            Assert.IsTrue(value.Contains("follow"));
        }

        [TestMethod]
        public void ConvertReadWriteFollow()
        {
            var enumWithNoFlag = AppScopeEnum.Read | AppScopeEnum.Write | AppScopeEnum.Follow;
            var value = AppScopesConverter.GetScopes(enumWithNoFlag);
            Assert.IsTrue(value.Contains("read"));
            Assert.IsTrue(value.Contains("write"));
            Assert.IsTrue(value.Contains("follow"));
        }
    }
}