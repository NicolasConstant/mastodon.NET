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

        public Account GetAccount(int userId, string accessToken)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(string.Format(ApiRoutes.GetAccount, userId), Method.GET);
            request.AddParameter("access_token", accessToken);

            var response = client.Execute(request);
            var content = response.Content;
            return JsonConvert.DeserializeObject<Account>(content);
        }
    }
}