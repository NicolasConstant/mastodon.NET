namespace mastodon.Consts
{
    public struct ApiRoutes
    {
        public static string CreateApp = "/api/v1/apps";
        public static string GetToken = "/oauth/token";
        public static string GetAccount = "/api/v1/accounts/{0}";
        public static string GetCurrentAccount = "/api/v1/accounts/verify_credentials";
    }
}