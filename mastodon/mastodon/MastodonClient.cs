﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            return GetAuthenticatedData<Account>(Method.GET, route, accessToken);
        }

        public Account GetCurrentAccount(string accessToken)
        {
            var route = ApiRoutes.GetCurrentAccount;
            return GetAuthenticatedData<Account>(Method.GET, route, accessToken);
        }

        public Account[] GetAccountFollowers(int accountId, int limit, string accessToken)
        {
            var route = string.Format(ApiRoutes.GetAccountFollowers, accountId);
            return GetAuthenticatedData<Account[]>(Method.GET, route, accessToken, limit);
        }

        public Account[] GetAccountFollowing(int accountId, string accessToken, int limit = -1)
        {
            var route = string.Format(ApiRoutes.GetAccountFollowing, accountId);
            return GetAuthenticatedData<Account[]>(Method.GET, route, accessToken, limit);
        }

        public Relationships[] GetAccountRelationships(int accountId, string accessToken)
        {
            var route = ApiRoutes.GetAccountRelationships;
            return GetAuthenticatedData<Relationships[]>(Method.GET, route, accessToken, -1, false, false, false, accountId);
        }

        public Account[] GetFollowRequests(string accessToken)
        {
            var route = ApiRoutes.GetFollowRequests;
            return GetAuthenticatedData<Account[]>(Method.GET, route, accessToken);
        }

        //TODO test this
        public void AuthorizeFollowRequest(int id, string accessToken)
        {
            var route = ApiRoutes.AuthorizeFollowRequest;
            GetAuthenticatedData<object>(Method.POST, route, accessToken, -1, false, false, false, id);
        }

        //TODO test this
        public void RejectFollowRequest(int id, string accessToken)
        {
            var route = ApiRoutes.RejectFollowRequest;
            GetAuthenticatedData<object>(Method.POST, route, accessToken, -1, false, false, false, id);
        }
        #endregion

        #region Actions
        public Relationships Follow(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Follow, accountId);
            return GetAuthenticatedData<Relationships>(Method.POST, route, accessToken);
        }

        public Relationships Unfollow(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unfollow, accountId);
            return GetAuthenticatedData<Relationships>(Method.POST, route, accessToken);
        }

        public Account FollowRemote(string uri, string accessToken)
        {
            var route = ApiRoutes.FollowRemote;
            return GetAuthenticatedData<Account>(Method.POST, route, accessToken, -1, false, false, false, -1, null, uri);
        }

        public Relationships Block(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Block, accountId);
            return GetAuthenticatedData<Relationships>(Method.POST, route, accessToken);
        }

        public Relationships Unblock(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unblock, accountId);
            return GetAuthenticatedData<Relationships>(Method.POST, route, accessToken);
        }

        public Account[] GetBlocks(string accessToken)
        {
            var route = string.Format(ApiRoutes.GetBlocks);
            return GetAuthenticatedData<Account[]>(Method.GET, route, accessToken);
        }

        public Relationships Mute(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Mute, accountId);
            return GetAuthenticatedData<Relationships>(Method.POST, route, accessToken);
        }

        public Account[] GetMutes(string accessToken)
        {
            var route = string.Format(ApiRoutes.GetMutes);
            return GetAuthenticatedData<Account[]>(Method.GET, route, accessToken);
        }

        public Relationships Unmute(int accountId, string accessToken)
        {
            var route = string.Format(ApiRoutes.Unmute, accountId);
            return GetAuthenticatedData<Relationships>(Method.POST, route, accessToken);
        }
        #endregion

        #region Notifications
        public Notification[] GetNotifications(string accessToken)
        {
            var route = ApiRoutes.GetNotifications;
            return GetAuthenticatedData<Notification[]>(Method.GET, route, accessToken);
        }

        public Notification GetSingleNotifications(string accessToken, int id)
        {
            var route = string.Format(ApiRoutes.GetSingleNotifications, id);
            return GetAuthenticatedData<Notification>(Method.GET, route, accessToken);
        }

        public void ClearNotifications(string accessToken)
        {
            var route = ApiRoutes.ClearNotifications;
            GetAuthenticatedData<object>(Method.POST, route, accessToken);
        }
        #endregion

        #region Timelines
        public Statuses[] GetAccountStatuses(int accountId, string accessToken, int limit = -1, bool onlyMedia = false, bool excludeReplies = false)
        {
            var route = string.Format(ApiRoutes.GetAccountStatuses, accountId);
            return GetAuthenticatedData<Statuses[]>(Method.GET, route, accessToken, limit, onlyMedia, excludeReplies);
        }

        public Statuses[] GetHomeTimeline(string accessToken)
        {
            var route = ApiRoutes.GetHomeTimeline;
            return GetAuthenticatedData<Statuses[]>(Method.GET, route, accessToken);
        }

        public Statuses[] GetPublicTimeline(string accessToken, bool local = false)
        {
            var route = ApiRoutes.GetPublicTimeline;
            return GetAuthenticatedData<Statuses[]>(Method.GET, route, accessToken, -1, false, false, local);
        }

        public Statuses[] GetHastagTimeline(string hashtag, string accessToken, bool local = false)
        {
            var route = string.Format(ApiRoutes.GetHastagTimeline, hashtag);
            return GetAuthenticatedData<Statuses[]>(Method.GET, route, accessToken, -1, false, false, local);
        }

        public Statuses[] GetFavorites(string accessToken)
        {
            var route = string.Format(ApiRoutes.GetFavourites);
            return GetAuthenticatedData<Statuses[]>(Method.GET, route, accessToken);
        }
        #endregion

        #region Search
        public Account[] SearchAccounts(string query, string accessToken, int limit = 40)
        {
            var route = ApiRoutes.SearchForAccounts;
            return GetAuthenticatedData<Account[]>(Method.GET, route, accessToken, limit, false, false, false, -1, query);
        }
        #endregion

        public Instance GetInstance()
        {
            var route = ApiRoutes.GetInstance;
            return GetAuthenticatedData<Instance>(Method.GET, route, string.Empty);
        }

        private T GetAuthenticatedData<T>(Method methodType, string route, string accessToken, int limit = -1, bool onlyMedia = false, bool excludeReplies = false, bool local = false, int id = -1, string query = null, string uri = null)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(route, methodType);

            if(!string.IsNullOrEmpty(accessToken))
                request.AddParameter("Authorization", string.Format("Bearer " + accessToken), ParameterType.HttpHeader);

            if (limit != -1) request.AddParameter("limit", limit);
            if (onlyMedia) request.AddParameter("only_media", "true");
            if (excludeReplies) request.AddParameter("exclude_replies", "true");
            if (local) request.AddParameter("local", "true");
            if (id != -1) request.AddParameter("id", id);
            if (!string.IsNullOrEmpty(query)) request.AddParameter("q", query);
            if (!string.IsNullOrEmpty(uri)) request.AddParameter("uri", uri);

            var response = client.Execute(request);
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}