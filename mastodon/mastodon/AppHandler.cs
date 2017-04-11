using System;
using mastodon.Consts;
using mastodon.Enums;
using mastodon.Models;
using mastodon.Tools;
using Newtonsoft.Json;
using RestSharp;

namespace mastodon
{
    public class AppHandler
    {
        private readonly string _url;

        #region Ctor
        public AppHandler(string url)
        {
            _url = url;
        }
        #endregion

        public AppInfo CreateApp(string clientName, string redirectUris, AppScopeEnum scopes, string website)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(ApiRoutes.CreateApp, Method.POST);
            request.AddParameter("client_name", clientName);
            request.AddParameter("redirect_uris", redirectUris);
            request.AddParameter("scopes", AppScopesConverter.GetScopes(scopes));
            request.AddParameter("website", website);
            request.AddHeader("Content-Type", "multipart/form-data");

            var response = client.Execute(request);
            var content = response.Content;
            return JsonConvert.DeserializeObject<AppInfo>(content);
        }
    }
}
