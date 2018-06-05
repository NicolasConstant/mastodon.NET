using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using mastodon.Enums;
using mastodon.Tools;
using Newtonsoft.Json;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace mastodon
{
    public partial class MastodonClient
    {
        private readonly string _url;

        #region Ctor
        public MastodonClient(string url)
        {
            _url = url;
        }
        #endregion

        private T GetAuthenticatedData<T>(RestParameters param)
        {
            var client = new RestClient(_url);
            var request = new RestRequest(param.Route, param.Type);

            if (param.HasAccessToken) request.AddParameter("Authorization", string.Format("Bearer " + param.AccessToken), ParameterType.HttpHeader);

            //TODO move params names to dedicated struct
            if (param.HasLimit) request.AddParameter("limit", param.Limit);
            if (param.OnlyMedia) request.AddParameter("only_media", "true");
            if (param.ExcludeReplies) request.AddParameter("exclude_replies", "true");
            if (param.Local) request.AddParameter("local", "true");
            if (param.HasId) request.AddParameter("id", param.Id);
            if (param.HasQuery) request.AddParameter("q", param.Query);
            if (param.HasUri) request.AddParameter("uri", param.Uri);
            if (param.HasStatus) request.AddParameter("status", param.Status);
            if (param.HasInReplyToId) request.AddParameter("in_reply_to_id", param.InReplyToId);
            if (param.HasMediaIds)
            {
                foreach (var id in param.MediaIds)
                {
                    request.AddParameter("media_ids[]", id);
                }
            }
            if (param.Sensitive) request.AddParameter("sensitive", "true");
            if (param.HasSpoilerText) request.AddParameter("spoiler_text", param.SpoilerText);
            if (param.HasVisibility) request.AddParameter("visibility", StatusVisibilityConverter.GetVisibility(param.Visibility));

            var response = client.Execute(request);
            var content = response.Result.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }

        private class RestParameters
        {
            public string Route { get; set; }
            public Method Type { get; set; }

            public string AccessToken { get; set; }
            public bool HasAccessToken { get { return !string.IsNullOrWhiteSpace(AccessToken); } }

            public int Limit { get; set; } = -1;
            public bool HasLimit { get { return Limit != -1; } }

            public bool OnlyMedia { get; set; }
            public bool ExcludeReplies { get; set; }
            public bool Local { get; set; }

            public int Id { get; set; } = -1;
            public bool HasId { get { return Id != -1; } }

            public string Query { get; set; }
            public bool HasQuery { get { return !string.IsNullOrWhiteSpace(Query); } }

            public string Uri { get; set; }
            public bool HasUri { get { return !string.IsNullOrWhiteSpace(Uri); } }

            public string Status { get; set; }
            public bool HasStatus { get { return !string.IsNullOrWhiteSpace(Status); } }

            public int InReplyToId { get; set; } = -1;
            public bool HasInReplyToId { get { return InReplyToId != -1; } }

            public int[] MediaIds { get; set; }
            public bool HasMediaIds { get { return MediaIds != null && MediaIds.Any(); } }

            public bool Sensitive { get; set; }

            public string SpoilerText { get; set; }
            public bool HasSpoilerText { get { return !string.IsNullOrWhiteSpace(SpoilerText); } }

            public StatusVisibilityEnum Visibility { get; set; } = StatusVisibilityEnum.Unknow;
            public bool HasVisibility { get { return Visibility != StatusVisibilityEnum.Unknow; } }
        }
    }
}