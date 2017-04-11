﻿using System.Linq;
using System.Net;
using mastodon.Enums;
using mastodon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class MastodonClientTests
    {
        [TestInitialize]
        public void TestInit()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
        }

        [TestMethod]
        public void GetAccount()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var account = client.GetAccount(1, tokenInfo.access_token);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public void GetCurrentAccount()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var account = client.GetCurrentAccount(tokenInfo.access_token);

            Assert.IsNotNull(account.url);
            Assert.IsNotNull(account.username);
        }

        [TestMethod]
        public void GetAccountFollowers()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var accounts = client.GetAccountFollowers(1, 4, tokenInfo.access_token);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }
        
        [TestMethod]
        public void GetAccountFollowing()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var accounts = client.GetAccountFollowing(1, tokenInfo.access_token, 4);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(4, accounts.Length);
        }

        [TestMethod]
        public void GetAccountStatuses()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var statuses1 = client.GetAccountStatuses(1, tokenInfo.access_token, 4);
            var statuses2 = client.GetAccountStatuses(1, tokenInfo.access_token, 4, true);
            var statuses3 = client.GetAccountStatuses(1, tokenInfo.access_token, 4, false, true);

            Assert.AreEqual(4, statuses1.Length);
            Assert.AreEqual(4, statuses2.Length);
            Assert.AreEqual(4, statuses3.Length);
        }

        [TestMethod]
        public void GetAccountRelationships()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var relationships = client.GetAccountRelationships(1, tokenInfo.access_token); //TODO pass a array Ids
            Assert.IsNotNull(relationships);
            Assert.IsTrue(relationships.First().id != default(int));
        }

        [TestMethod]
        public void GetHomeTimeline()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var timeline = client.GetHomeTimeline(tokenInfo.access_token);
        }

        [TestMethod]
        public void GetPublicTimeline()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var timeline1 = client.GetPublicTimeline(tokenInfo.access_token);
            var timeline2 = client.GetPublicTimeline(tokenInfo.access_token, true);
        }

        [TestMethod]
        public void GetHastagTimeline()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var timeline1 = client.GetHastagTimeline("mastodon", tokenInfo.access_token);
            var timeline2 = client.GetHastagTimeline("mastodon", tokenInfo.access_token, true);
        }

        [TestMethod]
        public void Follow()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceUrl);
            var followedAccount = client.Follow(1, tokenInfo.access_token);
            Assert.IsNotNull(followedAccount);
        }

        [TestMethod]
        public void Unfollow()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceUrl);
            var unfollowedAccount = client.Unfollow(1, tokenInfo.access_token);
            Assert.IsNotNull(unfollowedAccount);
        }

        [TestMethod]
        public void Block()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceUrl);
            var blockedAccount = client.Block(2, tokenInfo.access_token);
            Assert.IsNotNull(blockedAccount);
        }

        [TestMethod]
        public void Unblock()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceUrl);
            var unblockedAccount = client.Unblock(2, tokenInfo.access_token);
            Assert.IsNotNull(unblockedAccount);
        }

        [TestMethod]
        public void Mute()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceUrl);
            var mutedAccount = client.Mute(2, tokenInfo.access_token);
            Assert.IsNotNull(mutedAccount);
        }

        [TestMethod]
        public void Unmuted()
        {
            var tokenInfo = GetTokenInfo(AppScopeEnum.Follow);

            var client = new MastodonClient(Settings.InstanceUrl);
            var unmutedAccount = client.Unmute(2, tokenInfo.access_token);
            Assert.IsNotNull(unmutedAccount);
        }
        
        [TestMethod]
        public void SearchAccount()
        {
            var q = "ale";
            var tokenInfo = GetTokenInfo(AppScopeEnum.Read);

            var client = new MastodonClient(Settings.InstanceUrl);
            var accounts = client.SearchAccounts(q, tokenInfo.access_token);
            Assert.IsNotNull(accounts);
            Assert.AreEqual(40, accounts.Length);
            Assert.IsTrue(accounts.First().username.Contains(q));
        }

        private TokenInfo GetTokenInfo(AppScopeEnum scope)
        {
            var authHandler = new AuthHandler(Settings.InstanceUrl);
            var tokenInfo = authHandler.GetTokenInfo(Settings.ClientId, Settings.ClientSecret, Settings.UserLogin, Settings.UserPassword, scope);
            return tokenInfo;
        }
    }
}