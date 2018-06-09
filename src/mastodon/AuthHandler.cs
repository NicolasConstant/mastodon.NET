using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Enums;
using mastodon.Models;
using mastodon.Tools;
using Newtonsoft.Json;

namespace mastodon
{
    public class AuthHandler : IDisposable
    {
        private readonly string _instanceUrl;
        private readonly HttpClient _httpClient = new HttpClient();
        
        #region Ctor
        public AuthHandler(string instanceName)
        {
            _instanceUrl = $"https://{instanceName}";
        }
        #endregion

        public async Task<TokenInfo> GetTokenInfoAsync(string clientId, string clientSecret, string userEmail, string userPassword, AppScopeEnum scope)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("client_id", clientId));
            parameters.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            parameters.Add(new KeyValuePair<string, string>("grant_type", "password"));
            parameters.Add(new KeyValuePair<string, string>("username", userEmail));
            parameters.Add(new KeyValuePair<string, string>("password", userPassword));
            parameters.Add(new KeyValuePair<string, string>("scope", AppScopesConverter.GetScopes(scope)));
            
            var formUrlEncodedContent = new FormUrlEncodedContent(parameters);
            var url = _instanceUrl + ApiRoutes.GetToken;
            var response = await _httpClient.PostAsync(url, formUrlEncodedContent);

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenInfo>(content);
        }

        public string GetOauthCodeUrl(string clientId, AppScopeEnum scope, string redirectUris = "urn:ietf:wg:oauth:2.0:oob")
        {
            var getScope = Uri.EscapeDataString(AppScopesConverter.GetScopes(scope));
            var oauthCodeUrl = $"{_instanceUrl}/oauth/authorize?scope={getScope}&response_type=code&redirect_uri={redirectUris}&client_id={clientId}";
            return oauthCodeUrl;
        }

        public async Task<TokenInfo> GetTokenInfoAsync(string clientId, string clientSecret, string oauthCode, string redirectUris = "urn:ietf:wg:oauth:2.0:oob")
        {
            var accessTokenUrl = $"{_instanceUrl}/oauth/token?client_id={clientId}&client_secret={clientSecret}&grant_type=authorization_code&code={oauthCode}&redirect_uri={redirectUris}";

            var response = await _httpClient.PostAsync(accessTokenUrl, null);
            var oauthReturnJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenInfo>(oauthReturnJson);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}