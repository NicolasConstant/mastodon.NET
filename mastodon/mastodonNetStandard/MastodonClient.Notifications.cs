using mastodon.Consts;
using mastodon.Models;
using RestSharp.Portable;

namespace mastodon
{
    public partial class MastodonClient
    {
        public Notification[] GetNotifications(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = ApiRoutes.GetNotifications,
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Notification[]>(param);
        }

        public Notification GetSingleNotifications(string accessToken, int id)
        {
            var param = new RestParameters()
            {
                Type = Method.GET,
                Route = string.Format(ApiRoutes.GetSingleNotifications, id),
                AccessToken = accessToken
            };
            return GetAuthenticatedData<Notification>(param);
        }

        public void ClearNotifications(string accessToken)
        {
            var param = new RestParameters()
            {
                Type = Method.POST,
                Route = ApiRoutes.ClearNotifications,
                AccessToken = accessToken
            };
            GetAuthenticatedData<object>(param);
        }
    }
}