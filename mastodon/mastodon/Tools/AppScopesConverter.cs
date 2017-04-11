using mastodon.Consts;
using mastodon.Enums;

namespace mastodon.Tools
{
    public class AppScopesConverter
    {
        public static string GetScopes(AppScopeEnum scope)
        {
            var returnValue = "";

            if (scope.HasFlag(AppScopeEnum.Read)) returnValue += $" {AppScopes.Read}";
            if (scope.HasFlag(AppScopeEnum.Write)) returnValue += $" {AppScopes.Write}";
            if (scope.HasFlag(AppScopeEnum.Follow)) returnValue += $" {AppScopes.Follow}";

            return returnValue.Trim();
        }
    }
}