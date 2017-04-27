using System;

namespace mastodon.Enums
{
    [Flags]
    public enum AppScopeEnum
    {
        Read = 1,
        Write = 2,
        Follow = 4
    }
}