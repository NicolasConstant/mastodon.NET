using System;
using System.Collections.Generic;
using mastodon.Consts;
using mastodon.Models;
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
            var route = string.Format(ApiRoutes.GetAccount, accountId);
            return GetAuthenticatedData<Account>(route, accessToken);
        }

        public Account GetCurrentAccount(string accessToken)
        {
            var route = ApiRoutes.GetCurrentAccount;
            return GetAuthenticatedData<Account>(route, accessToken);
        }

        public Account[] GetAccountFollowers(int accountId, int limit, string accessToken)
        {
            var route = string.Format(ApiRoutes.GetAccountFollowers, accountId);
            return GetAuthenticatedData<Account[]>(route, accessToken, limit);
        }

        public Account[] GetAccountFollowing(int accountId, string accessToken, int limit = -1)
        {
            var route = string.Format(ApiRoutes.GetAccountFollowing, accountId);
            return GetAuthenticatedData<Account[]>(route, accessToken, limit);
        }
        #endregion

        #region Statuses
        public Statuses[] GetAccountStatuses(int accountId, string accessToken, int limit = -1, bool onlyMedia = false, bool excludeReplies = false)
        {
            var route = string.Format(ApiRoutes.GetAccountStatuses, accountId);
            return GetAuthenticatedData<Statuses[]>(route, accessToken, limit, onlyMedia, excludeReplies);
        }
        #endregion

        #region Actions
        public Account Follow(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Follow, accountId);
            return GetAuthenticatedData<Account>(route, accessToken);
        }

        public Account Unfollow(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unfollow, accountId);
            return GetAuthenticatedData<Account>(route, accessToken);
        }

        public Account Block(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Block, accountId);
            return GetAuthenticatedData<Account>(route, accessToken);
        }

        public Account Unblock(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unblock, accountId);
            return GetAuthenticatedData<Account>(route, accessToken);
        }

        public Account Mute(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Mute, accountId);
            return GetAuthenticatedData<Account>(route, accessToken);
        }

        public Account Unmute(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unmute, accountId);
            return GetAuthenticatedData<Account>(route, accessToken);
        }
        #endregion

        #region Notifications

        #endregion

        #region Timelines
        public Statuses[] GetHomeTimeline(string accessToken)
        {
            var route = ApiRoutes.GetHomeTimeline;
            return GetAuthenticatedData<Statuses[]>(route, accessToken);
        }

        public Statuses[] GetPublicTimeline(string accessToken, bool local = false)
        {
            var route = ApiRoutes.GetPublicTimeline;
            return GetAuthenticatedData<Statuses[]>(route, accessToken, -1, false, false, local);
        }

        public Statuses[] GetHastagTimeline(string hashtag, string accessToken, bool local = false)
        {
            var route = string.Format(ApiRoutes.GetHastagTimeline, hashtag);
            return GetAuthenticatedData<Statuses[]>(route, accessToken, -1, false, false, local);
        }
        #endregion

        private T GetAuthenticatedData<T>(string route, string accessToken, int limit = -1, bool onlyMedia = false, bool excludeReplies = false, bool local = false)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(route, Method.GET);
            request.AddParameter("access_token", accessToken);

            if (limit != -1) request.AddParameter("limit", limit);
            if (onlyMedia) request.AddParameter("only_media", "true");
            if (excludeReplies) request.AddParameter("exclude_replies", "true");
            if (local) request.AddParameter("local", "true");

            var response = client.Execute(request);
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}