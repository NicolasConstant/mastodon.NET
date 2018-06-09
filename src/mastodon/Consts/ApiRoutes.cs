namespace mastodon.Consts
{
    public struct ApiRoutes
    {
        public static string CreateApp = "/api/v1/apps";
        public static string GetToken = "/oauth/token";
        public static string GetAccount = "/api/v1/accounts/{0}";
        public static string GetCurrentAccount = "/api/v1/accounts/verify_credentials";
        public static string GetAccountFollowers = "/api/v1/accounts/{0}/followers";
        public static string GetAccountFollowing = "/api/v1/accounts/{0}/following";
        public static string GetAccountStatuses = "/api/v1/accounts/{0}/statuses";
        public static string Follow = "/api/v1/accounts/{0}/follow";
        public static string Unfollow = "/api/v1/accounts/{0}/unfollow";
        public static string Block = "/api/v1/accounts/{0}/block";
        public static string Unblock = "/api/v1/accounts/{0}/unblock";
        public static string Mute = "/api/v1/accounts/{0}/mute";
        public static string Unmute = "/api/v1/accounts/{0}/unmute";
        public static string GetAccountRelationships = "/api/v1/accounts/relationships";
        public static string SearchForAccounts = "/api/v1/accounts/search";
        public static string GetBlocks = "/api/v1/blocks";
        public static string GetFavourites = "/api/v1/favourites";
        public static string GetFollowRequests = "/api/v1/follow_requests";
        public static string AuthorizeFollowRequest = "/api/v1/follow_requests/authorize";
        public static string RejectFollowRequest = "/api/v1/follow_requests/reject";
        public static string FollowRemote = "/api/v1/follows";
        public static string GetInstance = "/api/v1/instance";
        public static string UploadMediaAttachment = "/api/v1/media";
        public static string GetMutes = "/api/v1/mutes";
        public static string GetNotifications = "/api/v1/notifications";
        public static string GetSingleNotifications = "/api/v1/notifications/{0}";
        public static string ClearNotifications = "/api/v1/notifications/clear";
        public static string GetReports = "/api/v1/reports";
        public static string ReportUser = "/api/v1/reports";
        public static string Search = "/api/v1/search";
        public static string GetStatus = "/api/v1/statuses/{0}";
        public static string GetStatusContext = "/api/v1/statuses/{0}/context";
        public static string GetStatusCard = "/api/v1/statuses/{0}/card";
        public static string GetStatusRebloggedBy = "/api/v1/statuses/{0}/reblogged_by";
        public static string GetStatusFavouritedBy = "/api/v1/statuses/{0}/favourited_by";
        public static string PostNewStatus = "/api/v1/statuses";
        public static string DeleteStatus = "/api/v1/statuses/{0}";
        public static string ReblogStatus = "/api/v1/statuses/{0}/reblog";
        public static string UnreblogStatus = "/api/v1/statuses/{0}/unreblog";
        public static string PinStatus = "/api/v1/statuses/{0}/pin";
        public static string UnpinStatus = "/api/v1/statuses/{0}/unpin";
        public static string MuteStatusConversation = "/api/v1/statuses/{0}/mute";
        public static string UnmuteStatusConversation = "/api/v1/statuses/{0}/unmute";
        public static string FavouritingStatus = "/api/v1/statuses/{0}/favourite";
        public static string UnfavouritingStatus = "/api/v1/statuses/{0}/unfavourite";
        public static string GetHomeTimeline = "/api/v1/timelines/home";
        public static string GetPublicTimeline = "/api/v1/timelines/public";
        public static string GetHastagTimeline = "/api/v1/timelines/tag/{0}";
    }
}