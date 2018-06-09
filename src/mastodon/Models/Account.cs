using System;
// ReSharper disable InconsistentNaming

namespace mastodon.Models
{
    public class Account
    {
        public string id { get; set; }
        public string username { get; set; }
        public string acct { get; set; }
        public string display_name { get; set; }
        public bool locked { get; set; }
        public DateTime created_at { get; set; }
        public string note { get; set; }
        public string url { get; set; }
        public string avatar { get; set; }
        public string header { get; set; }
        public int followers_count { get; set; }
        public int following_count { get; set; }
        public int statuses_count { get; set; }
    }
}