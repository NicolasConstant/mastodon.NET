using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using mastodon.Consts;
using mastodon.Enums;
using mastodon.Models;
using mastodon.Tools;
using Newtonsoft.Json;
using RestSharp;

namespace mastodon
{
    public class MastodonClient
    {
        private readonly string _url;

        #region Ctor
        public MastodonClient(string url)
        {
            _url = url;
        }
        #endregion

        #region Account Infos
        public Account GetAccount(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetAccount, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account>(param);
        }

        public Account GetCurrentAccount(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetCurrentAccount,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account>(param);
        }

        public Account[] GetAccountFollowers(int accountId, int limit, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetAccountFollowers, accountId),
                AccessToken = accessToken,
                Limit = limit
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Account[] GetAccountFollowing(int accountId, string accessToken, int limit = -1)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetAccountFollowing, accountId),
                AccessToken = accessToken,
                Limit = limit
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Relationships[] GetAccountRelationships(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetAccountRelationships,
                AccessToken = accessToken,
                Id = accountId
            };
            return GetAuthenticatedData<Relationships[]>(param);
        }

        public Account[] GetFollowRequests(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetFollowRequests,
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        //TODO test this
        public void AuthorizeFollowRequest(int id, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.AuthorizeFollowRequest,
                AccessToken = accessToken,
                Id = id
            };
            GetAuthenticatedData<object>(param);
        }

        //TODO test this
        public void RejectFollowRequest(int id, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.RejectFollowRequest,
                AccessToken = accessToken,
                Id = id
            };
            GetAuthenticatedData<object>(param);
        }
        #endregion

        #region Actions
        public Relationships Follow(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Follow, accountId),
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Relationships Unfollow(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Unfollow, accountId),
                AccessToken = accessToken,
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Account FollowRemote(string uri, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.FollowRemote,
                AccessToken = accessToken,
                Uri = uri,
            };
            return GetAuthenticatedData<Account>(param);
        }

        public Relationships Block(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Block, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Relationships Unblock(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Unblock, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Account[] GetBlocks(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetBlocks,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Relationships Mute(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Mute, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }

        public Account[] GetMutes(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetMutes,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Account[]>(param);
        }

        public Relationships Unmute(int accountId, string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = string.Format(ApiRoutes.Unmute, accountId),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Relationships>(param);
        }
        #endregion

        #region Notifications
        public Notification[] GetNotifications(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetNotifications,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Notification[]>(param);
        }

        public Notification GetSingleNotifications(string accessToken, int id)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetSingleNotifications, id),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Notification>(param);
        }

        public void ClearNotifications(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.ClearNotifications,
                AccessToken = accessToken
            };
            GetAuthenticatedData<object>(param);
        }
        #endregion

        #region Timelines
        public Status[] GetAccountStatuses(int accountId, string accessToken, int limit = -1, bool onlyMedia = false, bool excludeReplies = false)
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

        public Status PostNewStatus(string accessToken, string status, int inReplyToId = -1, int[] mediaIds = null, bool sensitive = false, string spoilerText = null, StatusVisibilityEnum visibility = StatusVisibilityEnum.Public)
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

        public void DeleteStatus(string accessToken, int statusId)
        {
            var param = new RestParameters()
            {
                Type = Method.DELETE,
                Route = string.Format(ApiRoutes.DeleteStatus, statusId),
                AccessToken = accessToken,
            };
            GetAuthenticatedData<object>(param);
        }

        public void ReblogStatus()
        {
            throw new NotImplementedException();
        }

        public void UnreblogStatus()
        {
            throw new NotImplementedException();
        }

        public void FavouritingStatus()
        {
            throw new NotImplementedException();
        }

        public void UnfavouritingStatus()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Search
        public Account[] SearchAccounts(string query, string accessToken, int limit = 40)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.SearchForAccounts,
                AccessToken = accessToken,
                Limit = limit,
                Query = query
            };
            return GetAuthenticatedData<Account[]>(param);
        }
        #endregion

        public Instance GetInstance()
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetInstance,
            };
            return GetAuthenticatedData<Instance>(param);
        }

        private T GetAuthenticatedData<T>(RestParameters param)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(param.Route, param.Type);

            if(param.HasAccessToken) request.AddParameter("Authorization", string.Format("Bearer " + param.AccessToken), ParameterType.HttpHeader);

            //TODO move params names to dedicated struct
            if (param.HasLimit) request.AddParameter("limit", param.Limit);
            if (param.OnlyMedia) request.AddParameter("only_media", "true");
            if (param.ExcludeReplies) request.AddParameter("exclude_replies", "true");
            if (param.Local) request.AddParameter("local", "true");
            if (param.HasId) request.AddParameter("id", param.Id);
            if (param.HasQuery) request.AddParameter("q", param.Query);
            if (param.HasUri) request.AddParameter("uri", param.Uri);
            if (param.HasStatus) request.AddParameter("status", param.Status);
            if (param.HasInReplyToId) request.AddParameter("in_reply_to_id", param.InReplyToId);
            if (param.HasMediaIds) request.AddParameter("media_ids", param.MediaIds); //TODO Format this correctly
            if (param.Sensitive) request.AddParameter("sensitive", "true");
            if (param.HasSpoilerText) request.AddParameter("spoiler_text", param.SpoilerText);
            if (param.HasVisibility) request.AddParameter("visibility", StatusVisibilityConverter.GetVisibility(param.Visibility));

            var response = client.Execute(request);
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }

        private class RestParameters
        {
            public string Route { get; set; }
            public Method Type { get; set; }

            public string AccessToken { get; set; }
            public bool HasAccessToken {get { return !string.IsNullOrWhiteSpace(AccessToken); } }

            public int Limit { get; set; } = -1;
            public bool HasLimit { get { return Limit != -1; } }

            public bool OnlyMedia { get; set; }
            public bool ExcludeReplies { get; set; }
            public bool Local { get; set; }

            public int Id { get; set; } = -1;
            public bool HasId { get { return Id != -1; } }

            public string Query { get; set; }
            public bool HasQuery { get { return !string.IsNullOrWhiteSpace(Query); } }
            
            public string Uri { get; set; }
            public bool HasUri { get { return !string.IsNullOrWhiteSpace(Uri); } }

            public string Status { get; set; }
            public bool HasStatus { get { return !string.IsNullOrWhiteSpace(Status);  } }

            public int InReplyToId { get; set; } = -1;
            public bool HasInReplyToId { get { return InReplyToId != -1; } }

            public int[] MediaIds { get; set; }
            public bool HasMediaIds { get { return MediaIds != null && MediaIds.Any(); } }

            public bool Sensitive { get; set; }
            
            public string SpoilerText { get; set; }
            public bool HasSpoilerText { get { return !string.IsNullOrWhiteSpace(SpoilerText); } }

            public StatusVisibilityEnum Visibility { get; set; } = StatusVisibilityEnum.Unknow;
            public bool HasVisibility { get { return Visibility != StatusVisibilityEnum.Unknow;  } }
        }
    }
}