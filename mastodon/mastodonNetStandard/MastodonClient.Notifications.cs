using System.Threading.Tasks;
using mastodon.Consts;
using mastodon.Models;

namespace mastodon
{
    public partial class MastodonClient
    {
        public async Task<Notification[]> GetNotificationsAsync(string accessToken)
        {
            return await GetDataAsync<Notification[]>(accessToken, ApiRoutes.GetNotifications);
        }

        public async Task<Notification> GetSingleNotificationsAsync(string accessToken, int id)
        {
            var route = string.Format(ApiRoutes.GetSingleNotifications, id);
            return await GetDataAsync<Notification>(accessToken, route);
        }

        public async Task ClearNotificationsAsync(string accessToken)
        {
            await GetDataAsync(accessToken, ApiRoutes.ClearNotifications);
        }
    }
}