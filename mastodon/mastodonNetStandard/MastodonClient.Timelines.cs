using System;
using System.Net.Http;
using System.Net.Http.Headers;
using mastodon.Consts;
using mastodon.Enums;
using mastodon.Models;
using Newtonsoft.Json;
using RestSharp.Portable;

namespace mastodon
{
    public partial class MastodonClient
    {
        public Status[] GetAccountStatuses(int accountId, string accessToken, int limit = -1, bool onlyMedia = false,
            bool excludeReplies = false)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetAccountStatuses, accountId),
                AccessToken = accessToken,
                Limit = limit,
                OnlyMedia = onlyMedia,
                ExcludeReplies = excludeReplies
            };
            return GetAuthenticatedData<Status[]>(param);
        }

        public Status[] GetHomeTimeline(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetHomeTimeline,
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Status[]>(param);
        }

        public Status[] GetPublicTimeline(string accessToken, bool local = false)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetPublicTimeline,
                AccessToken = accessToken,
                Local = local
            };
            return GetAuthenticatedData<Status[]>(param);
        }

        public Status[] GetHastagTimeline(string hashtag, string accessToken, bool local = false)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetHastagTimeline, hashtag),
                AccessToken = accessToken,
                Local = local
            };
            return GetAuthenticatedData<Status[]>(param);
        }

        public Status[] GetFavorites(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetFavourites),
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Status[]>(param);
        }

        public Status PostNewStatus(string accessToken, string status, int inReplyToId = -1, int[] mediaIds = null,
            bool sensitive = false, string spoilerText = null,
            StatusVisibilityEnum visibility = StatusVisibilityEnum.Public)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.PostNewStatus,
                AccessToken = accessToken,
                Status = status,
                InReplyToId = inReplyToId,
                MediaIds = mediaIds,
                Sensitive = sensitive,
                SpoilerText = spoilerText,
                Visibility = visibility
            };
            return GetAuthenticatedData<Status>(param);
        }

        public Attachment UploadingMediaAttachment(string accessToken, string description, byte[] mediaBytes,
            string mediaFileName)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var form = new MultipartFormDataContent();
                //form.Add(new StringContent(focus), "focus"); //TODO
                form.Add(new StringContent(description), "description");
                form.Add(new ByteArrayContent(mediaBytes, 0, mediaBytes.Length), "file",
                    mediaFileName);

                var response = httpClient.PostAsync(_url + ApiRoutes.UploadMediaAttachment, form).Result;
                response.EnsureSuccessStatusCode();

                var sd = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Attachment>(sd);
            }
        }

        public void DeleteStatus(string accessToken, string statusId)
        {
            var param = new RestParameters()
            {
                Type = Method.DELETE,
                Route = string.Format(ApiRoutes.DeleteStatus, statusId),
                AccessToken = accessToken,
            };
            GetAuthenticatedData<object>(param);
        }

        public Status ReblogStatus(string accessToken, int statusId)
        {
            throw new NotImplementedException();
        }

        public Status UnreblogStatus(string accessToken, int statusId)
        {
            throw new NotImplementedException();
        }

        public Status FavouritingStatus(string accessToken, int statusId)
        {
            throw new NotImplementedException();
        }

        public Status UnfavouritingStatus(string accessToken, int statusId)
        {
            throw new NotImplementedException();
        }
    }
}