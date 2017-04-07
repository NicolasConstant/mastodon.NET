using System;
using mastodon.Consts;
using mastodon.Enums;
using mastodon.Models;
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

        public AppInfo CreateApp(string clientName, string redirectUris, AppScopesEnum scopes, string website)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(ApiRoutes.CreateApp, Method.POST);
            request.AddParameter("client_name", clientName);
            request.AddParameter("redirect_uris", redirectUris);
            request.AddParameter("scopes", GetScopes(scopes));
            request.AddParameter("website", website);
            request.AddHeader("Content-Type", "multipart/form-data");

            var response = client.Execute(request);
            var content = response.Content;
            return JsonConvert.DeserializeObject<AppInfo>(content);
        }

        private string GetScopes(AppScopesEnum scope)
        {
            switch (scope)
            {
                case AppScopesEnum.Read: return AppScopes.Read;
                case AppScopesEnum.Write: return AppScopes.Write;
                case AppScopesEnum.Follow: return AppScopes.Follow;
                default: throw new ArgumentException("scope not found");
            }
        }
    }
}
