using System;
// ReSharper disable InconsistentNaming
namespace mastodon.Models
{
    public class Status
    {
        public string id { get; set; }
        public DateTime created_at { get; set; }
        public string in_reply_to_id { get; set; }
        public string in_reply_to_account_id { get; set; }
        public bool? sensitive { get; set; }
        public string spoiler_text { get; set; }
        public string visibility { get; set; }
        public Application application { get; set; }
        public Account account { get; set; }
        public Attachment[] media_attachments { get; set; }
        public Mention[] mentions { get; set; }
        public Tags[] tags { get; set; }
        public string uri { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public int reblogs_count { get; set; }
        public int favourites_count { get; set; }
        public Status reblog { get; set; }
        public bool? favourited { get; set; }
        public bool? reblogged { get; set; }
        public bool? pinned { get; set; }
        public bool? muted { get; set; }
    }
}