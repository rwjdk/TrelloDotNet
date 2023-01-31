﻿using System.Text.Json.Serialization;

namespace TrelloDotNet.Model
{
    public class Card
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        //todo - add more properties

    }
}