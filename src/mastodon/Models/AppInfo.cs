﻿// ReSharper disable InconsistentNaming
namespace mastodon.Models
{
    public class AppInfo
    {
        public int id { get; set; }
        public string redirect_uri { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}