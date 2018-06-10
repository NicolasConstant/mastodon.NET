using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using mastodon.Enums;
using mastodon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mastodon.Tests
{
    [TestClass]
    public class MastodonClientTimelinesTests
    {
        [TestInitialize]
        public void TestInit()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
        }

        [TestMethod]
        public async Task GetAccountStatuses()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var statuses1 = await client.GetAccountStatusesAsync("1", tokenInfo, 4);
            var statuses2 = await client.GetAccountStatusesAsync("1", tokenInfo, 4, true);
            var statuses3 = await client.GetAccountStatusesAsync("1", tokenInfo, 4, false, true);

            Assert.IsTrue(statuses1.Any());
            Assert.IsTrue(statuses2.Any());
            Assert.IsTrue(statuses3.Any());
        }

        [TestMethod]
        public async Task GetHomeTimeline()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var timeline = await client.GetHomeTimelineAsync(tokenInfo);
            Assert.IsNotNull(timeline);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(timeline.First().id));
        }

        [TestMethod]
        public async Task GetPublicTimeline()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var timeline1 = await client.GetPublicTimelineAsync(tokenInfo);
            Assert.IsNotNull(timeline1);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(timeline1.First().id));
            var timeline2 = await client.GetPublicTimelineAsync(tokenInfo, true);
            Assert.IsNotNull(timeline2);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(timeline2.First().id));
        }

        [TestMethod]
        public async Task GetHastagTimeline()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var timeline1 = await client.GetHastagTimelineAsync("mastodon", tokenInfo);
            Assert.IsNotNull(timeline1);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(timeline1.First().id));
            var timeline2 = await client.GetHastagTimelineAsync("mastodon", tokenInfo, true);
            Assert.IsNotNull(timeline2);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(timeline2.First().id));
        }

        [TestMethod]
        public async Task PostNewStatus()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var status = await client.PostNewStatusAsync(tokenInfo, "Cool status for testing purpose", StatusVisibilityEnum.Private, -1, null, true, "TESTING SPOILER");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.content));
        }

        [TestMethod]
        public async Task PostNewStatusWithMedia()
        {
            var imageUrl = "https://i.imgur.com/UpGhrQv.jpg";
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            
            //Get image bytes
            byte[] imageBytes;
            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(imageUrl);
            }

            //Upload Medias
            var mediasResult = await client.UploadingMediaAttachmentAsync(token, "Image Description", imageBytes, "screen.jgp");

            //Post Status 
            var result = await client.PostNewStatusAsync(token, "Hey check this!", StatusVisibilityEnum.Private, -1, new[] {mediasResult.id});
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result.id));
        }

        [TestMethod]
        public async Task DeleteStatus()
        {
            var tokenInfo = await GetAccessToken();

            var client = new MastodonClient(Settings.InstanceName);
            var status = await client.PostNewStatusAsync(tokenInfo, "Cool status for testing purpose", StatusVisibilityEnum.Private, -1, null, true, "TESTING SPOILER");

            await client.DeleteStatusAsync(tokenInfo, status.id);
        }
		
        [TestMethod]
        public async Task ReblogStatus()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var toot = await GetToot();

            var status = await client.ReblogStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.reblogged);
        }

        [TestMethod]
        public async Task UnreblogStatus()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var toot = await GetToot();

            var status = await client.ReblogStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.reblogged);

            var unreblogedStatus = await client.UnreblogStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(unreblogedStatus.id));
            Assert.IsFalse((bool)unreblogedStatus.reblogged);
        }

        [TestMethod]
        public async Task FavouritingStatus()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var toot = await GetToot();

            var status = await client.FavouritingStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.favourited);
        }

        [TestMethod]
        public async Task UnfavouritingStatus()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var toot = await GetToot();

            var status = await client.FavouritingStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.favourited);

            var unreblogedStatus = await client.UnfavouritingStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(unreblogedStatus.id));
            Assert.IsFalse((bool)unreblogedStatus.favourited);
        }

        [TestMethod]
        public async Task PinStatus()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var userId = (await client.GetCurrentAccountAsync(token)).id;
            var toot = (await client.GetAccountStatusesAsync(userId, token, 1)).First();

            var status = await client.PinStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.pinned);
        }

        [TestMethod]
        public async Task UnpinStatus()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var userId = (await client.GetCurrentAccountAsync(token)).id;
            var toot = (await client.GetAccountStatusesAsync(userId, token, 1)).First();

            var status = await client.PinStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.pinned);

            var unreblogedStatus = await client.UnpinStatusAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(unreblogedStatus.id));
            Assert.IsFalse((bool)unreblogedStatus.pinned);
        }

        [TestMethod]
        public async Task MuteStatusConversation()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var toot = await GetToot();

            var status = await client.MuteStatusConversationAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.muted);
        }

        [TestMethod]
        public async Task UnmuteStatusConversation()
        {
            var token = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var toot = await GetToot();

            var status = await client.MuteStatusConversationAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(status.id));
            Assert.IsTrue((bool)status.muted);

            var unreblogedStatus = await client.UnmuteStatusConversationAsync(token, toot.id);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(unreblogedStatus.id));
            Assert.IsFalse((bool)unreblogedStatus.muted);
        }

        private async Task<Status> GetToot()
        {
            var tokenInfo = await GetAccessToken();
            var client = new MastodonClient(Settings.InstanceName);
            var timeline = await client.GetPublicTimelineAsync(tokenInfo);
            return timeline.First();
        }

        private async Task<string> GetAccessToken()
        {
            if (string.IsNullOrWhiteSpace(Settings.Token))
            {
                var authHandler = new AuthHandler(Settings.InstanceName);
                var tokenInfo = await authHandler.GetTokenInfoAsync(Settings.ClientId, Settings.ClientSecret, Settings.UserEmail, Settings.UserPassword, AppScopeEnum.Write | AppScopeEnum.Read | AppScopeEnum.Follow);
                Settings.Token = tokenInfo.access_token;
            }

            return Settings.Token;
        }
    }
}