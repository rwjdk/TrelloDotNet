using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;
using TrelloDotNet.Control;

namespace TrelloDotNet.Model
{
    public class Card : RawJsonObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("idShort")]
        public int IdShort { get; set; }

        [JsonPropertyName("idBoard")]
        public string BoardId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("desc")]
        public string Description { get; set; }
        
        [JsonPropertyName("closed")]
        public bool Closed { get; set; }

        [JsonPropertyName("pos")]
        public int Position { get; set; }

        [JsonPropertyName("idList")]
        public string ListId { get; set; }

        [JsonIgnore] public DateTimeOffset Created => IdToCreatedHelper.GetCreatedFromId(Id);

        [JsonPropertyName("dateLastActivity")]
        public DateTimeOffset LastActivity { get; set; }
        
        [JsonPropertyName("due")]
        public DateTimeOffset? Due { get; set; }

        [JsonPropertyName("dueComplete")]
        public bool DueComplete { get; set; }

        [JsonPropertyName("labels")]
        public List<Label> Labels { get; set; }
        
        [JsonPropertyName("idChecklists")]
        public List<string> ChecklistIds { get; set; }
        
        [JsonPropertyName("idMembers")]
        public List<string> MemberIds { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("shortUrl")]
        public string ShortUrl { get; set; }

        [JsonPropertyName("cover")]
        public CardCover Cover { get; set; }

        //todo - add more properties

        public override string ToString()
        {
            return $"{Name} (Id: {Id})";
        }
    }
}