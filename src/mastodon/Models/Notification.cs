using System;
// ReSharper disable InconsistentNaming

namespace mastodon.Models
{
    public class Notification
    {
        public int id { get; set; }
        public string type { get; set; }
        public DateTime created_at { get; set; }
        public Account account { get; set; }
    }
}