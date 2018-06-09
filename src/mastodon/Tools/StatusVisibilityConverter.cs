using System;
using System.Threading;
using mastodon.Consts;
using mastodon.Enums;

namespace mastodon.Tools
{
    public class StatusVisibilityConverter
    {
        public static string GetVisibility(StatusVisibilityEnum visibility)
        {
            switch (visibility)
            {
                case StatusVisibilityEnum.Public: return StatusVisibility.Public;
                case StatusVisibilityEnum.Direct: return StatusVisibility.Direct;
                case StatusVisibilityEnum.Private: return StatusVisibility.Private;
                case StatusVisibilityEnum.Unlisted: return StatusVisibility.Unlisted;
                default: throw new ArgumentException("StatusVisibilityEnum value not supported");
            }
        }
    }
}