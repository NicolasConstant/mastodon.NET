using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mastodon
{
    public partial class MastodonClient : IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _mastodonInstance;

        #region Ctor
        public MastodonClient(string mastodonInstance)
        {
            _mastodonInstance = mastodonInstance;
        }
        #endregion

        private async Task<T> GetDataAsync<T>(string accessToken, string route, IEnumerable<KeyValuePair<string,string>> parameters = null)
        {
            var responseString = await GetDataAsync(accessToken, route, parameters);
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        private async Task<string> GetDataAsync(string accessToken, string route, IEnumerable<KeyValuePair<string, string>> parameters = null)
        {
            if(!string.IsNullOrWhiteSpace(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var url = $"https://{_mastodonInstance}{route}";
            if (parameters != null) url += "?" + string.Join("&", parameters.Select(kvp => kvp.Key + "=" + kvp.Value));

            return await _httpClient.GetStringAsync(url);
        }

        private async Task<T> PostDataAsync<T>(string accessToken, string route, IEnumerable<KeyValuePair<string, string>> content = null)
        {
            var responseString = await PostDataAsync(accessToken, route, content);
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        private async Task<string> PostDataAsync(string accessToken, string route, IEnumerable<KeyValuePair<string, string>> content = null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var url = $"https://{_mastodonInstance}{route}";

            var encodedContent = new FormUrlEncodedContent(content ?? Enumerable.Empty<KeyValuePair<string, string>>());
            var response = await _httpClient.PostAsync(url, encodedContent);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task DeleteDataAsync(string accessToken, string route)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var url = $"https://{_mastodonInstance}{route}";
            await _httpClient.DeleteAsync(url);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}