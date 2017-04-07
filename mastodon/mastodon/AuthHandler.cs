﻿using mastodon.Consts;
using mastodon.Models;
using Newtonsoft.Json;
using RestSharp;

namespace mastodon
{
    public class AuthHandler
    {
        private readonly string _url;

        #region Ctor
        public AuthHandler(string url)
        {
            _url = url;
        }
        #endregion

        public TokenInfo GetTokenInfo(string clientId, string clientSecret, string userLogin, string userPassword)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(ApiRoutes.GetToken, Method.POST);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", userLogin);
            request.AddParameter("password", userPassword);
            request.AddHeader("Content-Type", "multipart/form-data");

            var response = client.Execute(request);
            var content = response.Content;
            return JsonConvert.DeserializeObject<TokenInfo>(content);
        }
    }
}