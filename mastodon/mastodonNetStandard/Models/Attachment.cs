// ReSharper disable InconsistentNaming

namespace mastodon.Models
{
    public class Attachment
    {
        public int id { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string remote_url { get; set; }
        public string preview_url { get; set; }
        public string text_url { get; set; }

    }
}