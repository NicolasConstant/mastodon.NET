// ReSharper disable InconsistentNaming
namespace mastodon.Models
{
    public class Relationships
    {
        public int id { get; set; }
        public bool following { get; set; }
        public bool followed_by { get; set; }
        public bool blocking { get; set; }
        public bool muting { get; set; }
        public bool requested { get; set; }
    }
}