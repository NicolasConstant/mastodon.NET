using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Enums;
using mastodon.Models;
using mastodon.Tools;
using Newtonsoft.Json;

namespace mastodon
{
    public partial class MastodonClient
    {
        public async Task<Status[]> GetAccountStatusesAsync(string accountId, string accessToken, int limit = -1, bool onlyMedia = false, bool excludeReplies = false)
        {
            var route = string.Format(ApiRoutes.GetAccountStatuses, accountId);

            var content = new List<KeyValuePair<string, string>>();
            content.Add(new KeyValuePair<string, string>("only_media", onlyMedia.ToString()));
            content.Add(new KeyValuePair<string, string>("exclude_replies", excludeReplies.ToString()));
            if (limit != -1) content.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            
            return await GetDataAsync<Status[]>(accessToken, route, content);
        }

        public async Task<Status[]> GetHomeTimelineAsync(string accessToken)
        {
            return await GetDataAsync<Status[]>(accessToken, ApiRoutes.GetHomeTimeline);
        }

        public async Task<Status[]> GetPublicTimelineAsync(string accessToken, bool local = false)
        {
            return await GetDataAsync<Status[]>(accessToken, ApiRoutes.GetPublicTimeline, new[] { new KeyValuePair<string, string>("local", local.ToString()) });
        }

        public async Task<Status[]> GetHastagTimelineAsync(string hashtag, string accessToken, bool local = false)
        {
            var route = string.Format(ApiRoutes.GetHastagTimeline, hashtag);
            return await GetDataAsync<Status[]>(accessToken, route, new[] { new KeyValuePair<string, string>("local", local.ToString()) });
        }

        public async Task<Status[]> GetFavoritesAsync(string accessToken)
        {
            return await GetDataAsync<Status[]>(accessToken, ApiRoutes.GetFavourites);
        }

        public async Task<Status> PostNewStatusAsync(string accessToken, string status, StatusVisibilityEnum visibility = StatusVisibilityEnum.Public, int inReplyToId = -1, int[] mediaIds = null, bool sensitive = false, string spoilerText = null)
        {
            var content = new List<KeyValuePair<string, string>>();
            content.Add(new KeyValuePair<string, string>("status", status));
            content.Add(new KeyValuePair<string, string>("sensitive", sensitive.ToString().ToLowerInvariant()));
            content.Add(new KeyValuePair<string, string>("visibility", StatusVisibilityConverter.GetVisibility(visibility)));

            if (inReplyToId != -1) content.Add(new KeyValuePair<string, string>("in_reply_to_id", inReplyToId.ToString()));
            if (!string.IsNullOrWhiteSpace(spoilerText)) content.Add(new KeyValuePair<string, string>("spoiler_text", spoilerText));
            
            if (mediaIds != null)
                foreach (var id in mediaIds)
                    content.Add(new KeyValuePair<string, string>("media_ids[]", id.ToString()));

            return await PostDataAsync<Status>(accessToken, ApiRoutes.PostNewStatus, content);
        }

        public async Task<Attachment> UploadingMediaAttachmentAsync(string accessToken, string description, byte[] mediaBytes,
            string mediaFileName)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var form = new MultipartFormDataContent();
            //form.Add(new StringContent(focus), "focus"); //TODO
            form.Add(new StringContent(description), "description");
            form.Add(new ByteArrayContent(mediaBytes, 0, mediaBytes.Length), "file",
                mediaFileName);

            var response = await _httpClient.PostAsync($"https://{_mastodonInstance}{ApiRoutes.UploadMediaAttachment}", form);
            response.EnsureSuccessStatusCode();

            var sd = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Attachment>(sd);
        }

        public async Task DeleteStatusAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.DeleteStatus, statusId);
            await DeleteDataAsync(accessToken, route);
        }

        public async Task<Status> ReblogStatusAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.ReblogStatus, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }

        public async Task<Status> UnreblogStatusAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.UnreblogStatus, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }

        public async Task<Status> FavouritingStatusAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.FavouritingStatus, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }

        public async Task<Status> UnfavouritingStatusAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.UnfavouritingStatus, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }

        public async Task<Status> PinStatusAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.PinStatus, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }

        public async Task<Status> UnpinStatusAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.UnpinStatus, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }

        public async Task<Status> MuteStatusConversationAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.MuteStatusConversation, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }

        public async Task<Status> UnmuteStatusConversationAsync(string accessToken, string statusId)
        {
            var route = string.Format(ApiRoutes.UnmuteStatusConversation, statusId);
            return await PostDataAsync<Status>(accessToken, route);
        }
    }
}