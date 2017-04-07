using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Enums;
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

        public string CreateApp(string clientName, string redirectUris, AppScopesEnum scopes, string website)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(ApiRoutes.CreateApp, Method.POST);
            request.AddParameter("client_name", clientName);
            request.AddParameter("redirect_uris", redirectUris);
            request.AddParameter("scopes", GetScopes(scopes));
            request.AddParameter("website", website);
            request.AddHeader("Content-Type", "multipart/form-data");

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            return content;
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
