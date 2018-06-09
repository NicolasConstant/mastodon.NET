using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Enums;
using mastodon.Models;
using mastodon.Tools;
using Newtonsoft.Json;

namespace mastodon
{
    public class AppHandler
    {
        private readonly string _instanceUrl;
        private readonly HttpClient _httpClient = new HttpClient();
        
        #region Ctor
        public AppHandler(string instanceName)
        {
            _instanceUrl = $"https://{instanceName}";
        }
        #endregion

        public async Task<AppInfo> CreateAppAsync(string clientName, string redirectUris, AppScopeEnum scopes, string website)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("client_name", clientName));
            parameters.Add(new KeyValuePair<string, string>("redirect_uris", redirectUris));
            parameters.Add(new KeyValuePair<string, string>("scopes", AppScopesConverter.GetScopes(scopes)));
            parameters.Add(new KeyValuePair<string, string>("website", website));
            
            var formUrlEncodedContent = new FormUrlEncodedContent(parameters);
            var url = _instanceUrl + ApiRoutes.CreateApp;
            var response = await _httpClient.PostAsync(url, formUrlEncodedContent);

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AppInfo>(content);
        }
    }
}
