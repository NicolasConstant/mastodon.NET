﻿// ReSharper disable InconsistentNaming
namespace mastodon.Models
{
    public class TokenInfo
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int created_at { get; set; }
    }
}